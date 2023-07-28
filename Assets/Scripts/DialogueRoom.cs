using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class DialogueRoom : MonoBehaviour
{
    bool hasTextChanged;
    public TMP_Text textBox, characterName;
    public List<string> linesMad, linesBen, linesBenaftMad;
    List<string> lines;
    public List<AudioSource> voice;
    string currentline, npcNameStr, questionLine;
    int currentlineInt;
    public GameObject skip, box, mc, npc, canvas, npcName, camSwitch;
    public int visibleCount = 0;
    public int linelength;
    bool madSpk, benSpk, mouthOp, useMouth;
    public Sprite mouthOpenMad, mouthClosedMad;
    Sprite mouthOpen, mouthClosed;
    SpriteRenderer mouth;
    private void OnEnable()
    {
        npcNameStr = npcName.GetComponent<currentNpc>().npc;
        if(npcNameStr == "Maddie"){
            mouth = GameObject.Find(npcNameStr+" mouth").GetComponent<SpriteRenderer>();
            useMouth = true;
            mouthOpen = mouthOpenMad;
            mouthClosed = mouthClosedMad;
            lines = linesMad;
            madSpk = true;
            camSwitch.GetComponent<CinemachineSwitcher>().Invoke("Maddie", 0f);
        }
        if(npcNameStr == "Benji"){
            useMouth = false;
            lines = linesBen;
            if(!benSpk){
                benSpk = true;
            }else{
                if(madSpk){
                    lines = linesBenaftMad;
                    string save = Application.dataPath + "/game.sg";
                    Debug.Log(save);
                    string linesSave = File.ReadAllText(save).ToString();
                    string[] linesArray = linesSave.Split('\n');
                    linesArray[2] = (int.Parse(linesArray[2]) + 1).ToString();
                    File.WriteAllLines(save, linesArray);
                }
            }
            camSwitch.GetComponent<CinemachineSwitcher>().Invoke("Benji", 0f);
        }
        characterName.text = npcNameStr;
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
        int totalVisibleCharacters = textInfo.characterCount; // get # of visible character in text object
        visibleCount = 0;

        while (true)
        {
            if (hasTextChanged)
            {
                totalVisibleCharacters = textInfo.characterCount; // update visible character count.
                hasTextChanged = false;
            }

            if (visibleCount == totalVisibleCharacters)
            {
                mouthOp = false;
                if (Input.GetKey(KeyCode.Return))
                {
                    currentlineInt += 1;
                    if(currentlineInt < lines.Count){
                        textBox.text = lines[currentlineInt];
                        currentline = lines[currentlineInt];
                        totalVisibleCharacters = currentline.Length;
                        visibleCount = 0;
                        skip.SetActive(false);
                    }else{
                        box.GetComponent<Animation>()["openAnim"].speed = -1f;
                        box.GetComponent<Animation>()["openAnim"].time = box.GetComponent<Animation>()["openAnim"].length;
                        box.GetComponent<Animation>().Play("openAnim");
                        camSwitch.GetComponent<CinemachineSwitcher>().ResetCam();
                        StartCoroutine(disableBox());
                    }
                }
                else
                {
                    skip.SetActive(true);
                }
            }
            if (visibleCount < currentline.Length)
            {
                mouthOp = true;
                playVoice();
            }
            textComponent.maxVisibleCharacters = visibleCount;
            if(visibleCount < currentline.Length)
            {
                visibleCount += 1;
            }
            yield return new WaitForSeconds(0.045f);
        }
    }
    void playVoice()
    {
        voice[Random.Range(0, voice.Count)].Play();
    }
    IEnumerator disableBox(){
        yield return new WaitForSeconds(0.4f);
        mc.GetComponent<playerMovementRooms>().stop = false;
        canvas.SetActive(false);
    }
    void LateUpdate()
    {
        if(useMouth){
            if(mouthOp){
                mouth.sprite = mouthOpen;
            }else{
                mouth.sprite = mouthClosed;
            }
        }
    }
}