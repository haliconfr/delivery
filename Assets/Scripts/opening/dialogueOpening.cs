using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueOpening : MonoBehaviour
{
    bool hasTextChanged;
    public TMP_Text textBox;
    public List<string> lines;
    public List<AudioSource> voice;
    string currentline;
    int currentlineInt;
    public GameObject skip, box, canvas, fOut;
    public Sprite mouthClosed, mouthOpen;
    public SpriteRenderer mouth;
    public int visibleCount = 0;
    public int linelength;
    bool enter;
    private void OnEnable()
    {
        currentlineInt = 0;
        currentline = lines[currentlineInt];
        textBox.text = lines[currentlineInt];
        StartCoroutine(RevealCharacters(textBox));
        box.GetComponent<Animation>()["openAnim"].speed = 1f;
        box.GetComponent<Animation>()["openAnim"].time = box.GetComponent<Animation>()["openAnim"].length;
        box.GetComponent<Animation>().Play("openAnim");
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

            if (visibleCount == totalVisibleCharacters)
            {
                mouth.sprite = mouthClosed;
                if (enter)
                {
                    currentlineInt += 1;
                    if(currentlineInt < lines.Count){
                        skip.SetActive(false);
                        textBox.text = lines[currentlineInt];
                        currentline = lines[currentlineInt];
                        totalVisibleCharacters = currentline.Length;
                        visibleCount = 0;
                    }else{
                        box.GetComponent<Animation>()["openAnim"].speed = -1f;
                        box.GetComponent<Animation>()["openAnim"].time = box.GetComponent<Animation>()["openAnim"].length;
                        box.GetComponent<Animation>().Play("openAnim");
                        StartCoroutine(disableBox());
                        fOut.SetActive(true);
                    }
                }else{
                    skip.SetActive(true);
                }
            }
            if (visibleCount < currentline.Length)
            {
                playVoice();
            }
            textComponent.maxVisibleCharacters = visibleCount;
            if(visibleCount < currentline.Length)
            {
                visibleCount += 1;
            }
            yield return new WaitForSeconds(0.06f);
        }
    }
    void playVoice()
    {
        voice[Random.Range(0, voice.Count)].Play();
        mouth.sprite = mouthOpen;
    }
    IEnumerator disableBox(){
        yield return new WaitForSeconds(0.3f);
        canvas.SetActive(false);
        yield return new WaitForSeconds(2f);
    }
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Return))
    {
        enter = true;
    }
    if (Input.GetKeyUp(KeyCode.Return))
    {
        enter = false;
    }
    }
}
