using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startOcean : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player"){
            this.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player"){
            this.gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }
}
