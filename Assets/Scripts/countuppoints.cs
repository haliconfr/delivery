using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class countuppoints : MonoBehaviour
{
    public TMP_Text pts;
    int points;
    public AudioSource tick;
    void Start()
    {
        StartCoroutine(countup());
        Debug.Log("started");
        tick.Play();
    }
    IEnumerator countup(){
        yield return new WaitForSeconds(0.02f);
        if(points <= 100){
            pts.text = points.ToString() + "pts";
            points++;
            StartCoroutine(countup());
        }else{
            tick.Stop();
        }
        Debug.Log("count");
    }
}
