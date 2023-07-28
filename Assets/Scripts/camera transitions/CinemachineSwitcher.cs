using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator animator;
    bool FreeLook = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Benji(){
        animator.Play("DialogueCam");
    } 
    void Maddie(){
        animator.Play("DialogueCam2");
    }
    public void ResetCam()
    {
        animator.Play("FreeCam");
    }
}
