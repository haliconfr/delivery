using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sittingIdle : MonoBehaviour
{
    Animator anim;
    public List<string> idles;
    string animStr;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        animStr = idles[Random.Range(0, 2)];
        anim.Play(animStr);
    }
}