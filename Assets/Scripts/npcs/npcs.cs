using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcs : MonoBehaviour
{
    public GameObject box;
    public Transform dialogRef, mc;
    Animator anim;
    void Start(){
        anim = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    public void dialogue(){
        anim.Play("talking");
        StopAllCoroutines();
        box.SetActive(true);
        mc.LookAt(dialogRef);
        mc.GetComponent<playerMovement>().stop = true;
    }
}
