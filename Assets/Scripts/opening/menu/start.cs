using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class start : MonoBehaviour
{
    public GameObject LevelLoad, fadeout;
    public string[] saveData;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        LevelLoad = GameObject.Find("loadvar");
        string save = Application.dataPath + "/game.sg";
        if(!File.Exists(save)){
            File.Create(save);
            saveData[0] = 150.ToString();
            saveData[1] = 0.ToString();
            saveData[2] = 0.ToString();
            File.WriteAllLines(save, saveData);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            StartCoroutine(startGame());
        }
    }
    IEnumerator startGame(){
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2f);
        LevelLoad.GetComponent<loadLevel>().called("companyIsland");
    }
}
