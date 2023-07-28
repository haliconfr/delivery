using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public GameObject credit;
    bool open;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            if(open){
                credit.SetActive(false);
                open = false;
            }else{
                credit.SetActive(true);
                open = true;
            }
        }
    }
}
