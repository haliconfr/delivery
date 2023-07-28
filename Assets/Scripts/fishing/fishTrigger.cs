using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishTrigger : MonoBehaviour
{
    public bool foundFish;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "fish"){
            foundFish = true;
        }
    }
}