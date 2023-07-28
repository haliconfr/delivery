using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcCasino : MonoBehaviour
{
    int timesChecked;
    public GameObject box;
    public Transform mc;
    public void dialogue(){
        box.SetActive(true);
        mc.GetComponent<movementCasino>().stop = true;
    }
}