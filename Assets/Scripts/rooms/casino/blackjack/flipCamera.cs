using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipCamera : MonoBehaviour
{
    public GameObject cam;
    public Animator camAnim;
    int state;
    void Start(){
        state = 0;
    }
    public void flipDown()
    {
        if(state == 0){
            camAnim.CrossFade("flip down", 0.5f);
            state = 1;
        }
    }
    public void flipUp(){
        if(state == 1){
            camAnim.CrossFade("flip up", 0.5f);
            state = 0;
        }
    }
}
