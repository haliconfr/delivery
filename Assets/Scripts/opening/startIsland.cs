using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startIsland : MonoBehaviour
{
    GameObject loadlevel;
    void Start()
    {
        loadlevel = GameObject.Find("loadvar");
        StartCoroutine(load());
    }
    IEnumerator load(){
        yield return new WaitForSeconds(1f);
        loadlevel.GetComponent<loadLevel>().called("grassyIsland");
    }
}
