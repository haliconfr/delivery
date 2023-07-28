using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlePlayer : MonoBehaviour
{
    Animation animator;
    public GameObject mc;
    void Start()
    {
        animator = mc.GetComponent<Animation>();
    }
    void FixedUpdate()
    {
        if(!animator.IsPlaying("idle")){
            animator.CrossFade("idle", 0.2f);
        }
    }
}
