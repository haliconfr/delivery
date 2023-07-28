using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoLand : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "auto land"){
            GameObject.Find("Camera").GetComponent<cameraMoveTop>().autoLand();
            Destroy(collider.gameObject);
        }
    }
}
