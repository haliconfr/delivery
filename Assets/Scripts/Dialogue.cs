using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    bool hasTextChanged;
    public TMP_Text textBox;
    public List<string> lines;
    public List<AudioSource> voice;
    string currentline;
    int currentlineInt;
    public GameObject skip, box, mc, npc, camswitch, canvas;
    public SpriteRenderer mouth;
    public Sprite mouthClosed, mouthOpen;
    public int visibleCount = 0;
    public int linelength;
    bool enter;
    private void OnEnable()
    {
        currentlineInt = 0;
        camswitch.GetComponent<CinemachineSwitcherOV>().Maddie();
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
                mouth.sprite = mouthClosed;
                if (enter)
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
                        camswitch.GetComponent<CinemachineSwitcherOV>().fishing();
                        StartCoroutine(disableBox());
                    }
                }
                skip.SetActive(true);
            }
            if (visibleCount < currentline.Length)
            {
                playVoice();
                mouth.sprite = mouthOpen;
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
        mc.GetComponent<playerMovement>().stop = false;
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
