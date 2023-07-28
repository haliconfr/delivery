using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class npcsRoom : MonoBehaviour
{
    int timesChecked;
    public GameObject box;
    public Transform mc;
    public void dialogue(){
        box.SetActive(true);
        mc.GetComponent<playerMovementRooms>().stop = true;
    }
}

