using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morecups : MonoBehaviour
{
    public Transform cupspot;
    public GameObject cup;
    void addCup(){
        GameObject cupClone = Instantiate(cup);
        cupClone.transform.position = cupspot.position;
    }
}