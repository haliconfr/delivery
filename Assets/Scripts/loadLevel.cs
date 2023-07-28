using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
{
    public bool dropped, exiting;
    int betAmount;
    public bool task1;
    public List<AudioClip> music;
    public int weather;
    GameObject package;
    float musicTime;
    public void called(string levelCall)
    {
        DontDestroyOnLoad(this.gameObject);
        if(levelCall == "casino lobby"){
            GameObject.Find("music").GetComponent<AudioSource>().Pause();
        }
        if(levelCall == "betting"){
            if(GameObject.Find("casinoRadio") != null){
                DontDestroyOnLoad(GameObject.Find("casinoRadio"));
            }
        }
        if(SceneManager.GetActiveScene().name == "cafe"){
            exiting = true;
        }
        SceneManager.LoadScene("loadScene");
        StartCoroutine(LoadSceneAsync(levelCall));
    }
    IEnumerator LoadSceneAsync(string levelToLoad){
        yield return new WaitForSeconds(2);
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(levelToLoad);
        loadAsync.completed += loadcompleted;
    }
    private void loadcompleted(AsyncOperation async){
        if(SceneManager.GetActiveScene().name == "grassyIsland" | SceneManager.GetActiveScene().name == "menu" | SceneManager.GetActiveScene().name == "companyIsland"){
            if(weather == 1){
                GameObject.Find("weatherSet").GetComponent<setRaining>().setToRain();
                if(GameObject.Find("music").GetComponent<AudioSource>().clip != music[1]){
                    GameObject.Find("music").GetComponent<AudioSource>().clip = music[1];
                    GameObject.Find("music").GetComponent<AudioSource>().Play();
                }
            }
            if(exiting){
                GameObject mc = GameObject.Find("mc");
                mc.transform.rotation = new Quaternion(mc.transform.rotation.x, 180, mc.transform.rotation.z, mc.transform.rotation.w);
                GameObject.Find("planeBase").SetActive(false);
                GameObject.Find("planestatic").SetActive(true);
                GameObject.Find("shift to land").SetActive(false);
                GameObject.Find("CM StateDrivenCamera1").GetComponent<Animator>().Play("character");
                mc.transform.position = GameObject.Find("exitCafePoint").transform.position;
            }else{
                GameObject.Find("planestatic").SetActive(false);
                if(SceneManager.GetActiveScene().name == "grassyIsland"){
                    GameObject.Find("mc").SetActive(false);
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "cafe"){
            if(weather == 1){
                GameObject.Find("rainAmbience").SetActive(true);
            }else{
                GameObject.Find("rainAmbience").SetActive(false);
            }
            GameObject.Find("music").GetComponent<AudioSource>().clip = music[2];
            GameObject.Find("music").GetComponent<AudioSource>().Play();
        }
        if(SceneManager.GetActiveScene().name == "blackjack"){
            if(GameObject.Find("casinoRadio") != null){
                musicTime = GameObject.Find("casinoRadio").GetComponent<AudioSource>().time;
                Destroy(GameObject.Find("casinoRadio"));
                GameObject.Find("cardMusic").GetComponent<AudioSource>().time = musicTime;
                GameObject.Find("cardMusic").GetComponent<AudioSource>().Play();
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}