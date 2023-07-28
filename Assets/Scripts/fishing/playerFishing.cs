using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFishing : MonoBehaviour
{
    public Animator playerAnimations;
    bool casting, cast, mashing;
    public GameObject trigger;
    bool good;
    public float meter;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(!cast){
                playerAnimations.Play("cast");
                casting = true;
                cast = true;
            }
        }
        //if(casting){
        //    if(!playerAnimations.GetCurrentAnimatorStateInfo(0).IsName("cast")){
        //        Debug.Log("play idle");
        //        casting = false;
        //        playerAnimations.Play("fishing idle");
        //    }
        //}
        if(trigger.GetComponent<fishTrigger>().foundFish == true){
            playerAnimations.SetBool("pull out", true);
            StartCoroutine(mashFish());
            StartCoroutine(catchFish());
            StartCoroutine(cooldown());
        }
        if(mashing){
            if(Input.GetKeyDown(KeyCode.E)){
            meter = meter + 0.4f;
            if(meter >= Random.Range(10,15)){
                playerAnimations.SetBool("finish mash", true);
                StopCoroutine(catchFish());
            }
        }
    }
    }
    IEnumerator mashFish(){
        yield return new WaitForEndOfFrame();
        Debug.Log("mash");
        mashing = true;
        StartCoroutine(mashFish());
    }
    IEnumerator catchFish(){
        yield return new WaitForSeconds(Random.Range(5, 7));
        fail();
    }
    IEnumerator cooldown(){
        yield return new WaitForSeconds(1f);
        meter = meter - 0.01f;
        if(meter == 0){
            fail();
        }
        StartCoroutine(cooldown());
    }
    void fail(){
        mashing = false;
        Debug.Log("fail");
        playerAnimations.SetBool("pull out", false);
        playerAnimations.SetBool("mash failed", true);
        StopCoroutine(catchFish());
        StopCoroutine(mashFish());
        StopCoroutine(cooldown());
        UnityEditor.EditorApplication.isPlaying = false;
    }
}