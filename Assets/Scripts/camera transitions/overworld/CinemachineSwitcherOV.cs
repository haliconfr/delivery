using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcherOV : MonoBehaviour
{
    private Animator animator;
    bool plane = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Maddie(){
        animator.Play("dialogueMaddie");
    }
    public void player(){
        animator.Play("character");
    }
    public void resetCam(){
        animator.Play("character");
    }
    public void fishing(){
        animator.Play("leaderboard");
    }
}
