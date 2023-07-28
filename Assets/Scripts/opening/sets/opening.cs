using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class opening : MonoBehaviour
{
    public string appPath;
    public GameObject load;
    void Start()
    {
        int day = System.DateTime.Now.Day;
        if(day % 4 == 0){
            load.GetComponent<loadLevel>().weather = 1;
        }else{
            load.GetComponent<loadLevel>().weather = 0;
        }
        appPath = Application.dataPath + "/cat.png";
        StartCoroutine(wait());
    }
    IEnumerator wait(){
        yield return new WaitForSeconds(4.2f);
        if(!File.Exists(appPath)){
            Application.Quit(2);
        }else{
            load.GetComponent<loadLevel>().called("menu");
        }
    }
}
