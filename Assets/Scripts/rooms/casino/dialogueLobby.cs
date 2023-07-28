using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogueLobby : MonoBehaviour
{
    bool hasTextChanged;
    public TMP_Text textBox, characterName;
    public List<string> lines;
    public List<AudioSource> voice;
    string currentline;
    public string npcNameStr;
    int currentlineInt;
    public GameObject skip, box, mc, npc, canvas, camSwitch;
    public int visibleCount = 0;
    public int linelength;
    bool mouthOp;
    public Sprite mouthOpen, mouthClosed;
    public SpriteRenderer mouth;
    private void OnEnable()
    {
        camSwitch.GetComponent<Animator>().Play("dialogue");
        characterName.text = npcNameStr;
        currentlineInt = 0;
        currentline = lines[currentlineInt];
        textBox.text = lines[currentlineInt];
        StartCoroutine(RevealCharacters(textBox));
        box.GetComponent<Animation>()["openAnim"].speed = 1f;
        box.GetComponent<Animation>()["openAnim"].time = box.GetComponent<Animation>()["openAnim"].length;
        box.GetComponent<Animation>().Play("openAnim");
        npc.GetComponent<Animator>().CrossFade("happy", 0.1f);
    }
    IEnumerator RevealCharacters(TMP_Text textComponent)
    {
        Debug.Log(currentlineInt);
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
                    if(currentlineInt == 4){
                        npc.GetComponent<Animator>().CrossFade("checking", 0.1f);
                    }
                    if(currentlineInt == 5){
                        npc.GetComponent<Animator>().CrossFade("idle", 0.3f);
                    }
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
                        camSwitch.GetComponent<Animator>().Play("FreeCam");
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
        mc.GetComponent<movementCasino>().stop = false;
        canvas.SetActive(false);
    }
    void LateUpdate()
    {
        if(mouthOp){
            mouth.sprite = mouthOpen;
        }else{
            mouth.sprite = mouthClosed;
        }
    }
}