using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ingameConsole : MonoBehaviour
{
    public GameObject consoleObj;
    public TMP_InputField console;
    bool consoleopen;
    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote)){
            if(consoleopen == false){

                consoleopen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                consoleObj.SetActive(true);
            }else{
                consoleopen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                consoleObj.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            if(consoleopen){
                string command = console.text;
                if(command.Contains("load")){
                    GameObject player = GameObject.Find("mc");
                    if(player != null){
                        player.GetComponent<playerMovement>().enabled = false;
                    }
                    GameObject.Find("loadvar").GetComponent<loadLevel>().called(command.Replace("load ", ""));
                    consoleObj.SetActive(false);
                    console.text = "";
                    consoleopen = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }
}
