using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class endingDialogue : MonoBehaviour
{
    bool hasTextChanged;
    public TMP_Text textBox;
    public string currentline;
    public int visibleCount = 0;
    public float speed;
    int currentlineInt;
    bool enter;
    public bool lost;
    public GameObject fade;
    List<string> saveLines;
    public void OnEnable()
    {
        GameObject.Find("Hit").SetActive(false);
        GameObject.Find("Stand").SetActive(false);
        StopAllCoroutines();
        string save = Application.dataPath + "/game.sg";
        if(lost){
            File.ReadAllLines(save);
            var readLines = File.ReadAllLines(save);
            saveLines = new List<string>(readLines);
            int amt = betAmount.wallet - betAmount.amount;
            if(amt <= 0){
                amt = 10;
            }
            currentline = "you lost!";
            saveLines[0] = (amt).ToString();
            File.WriteAllLines(save, saveLines);
        }else{
            File.ReadAllLines(save);
            var readLines = File.ReadAllLines(save);
            saveLines = new List<string>(readLines);
            currentline = "you won " + "$" + ((betAmount.amount * 2)) + "!";
            saveLines[0] = ((betAmount.amount * 2) + betAmount.wallet).ToString();
            File.WriteAllLines(save, saveLines);
        }
        textBox.text = currentline;
        StartCoroutine(RevealCharacters(textBox));
    }
    IEnumerator RevealCharacters(TMP_Text textComponent)
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;
        int totalVisibleCharacters = textInfo.characterCount;
        visibleCount = 0;

        while (true)
        {
            if (hasTextChanged)
            {
                totalVisibleCharacters = textInfo.characterCount;
                hasTextChanged = false;
            }
            if (visibleCount == currentline.Length)
            {
                if(enter){
                    StartCoroutine(fadeout());
                }
            }
            textComponent.maxVisibleCharacters = visibleCount;
            if(visibleCount < currentline.Length)
            {
                visibleCount += 1;
            }
            yield return new WaitForSeconds(speed);
        }
    }
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(rematch());
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            StartCoroutine(fadeout());
        }
    }
    IEnumerator fadeout(){
        fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("casino lobby");
    }
    IEnumerator rematch(){
        fade.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("betting");
    }
}
