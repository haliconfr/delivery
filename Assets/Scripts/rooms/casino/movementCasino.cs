using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementCasino : MonoBehaviour
{
    Rigidbody player;
    Animation animator;
    public GameObject mc, npcName, fadeout;
    public bool moving;
    public float speed, turnSpeed;
    public bool stop;
    float horizontalInput, verticalInput;
    void Start()
    {
        player = gameObject.GetComponent<Rigidbody>();
        animator = mc.GetComponent<Animation>();
    }
    void FixedUpdate()
    {
        if(horizontalInput != 0 || verticalInput != 0){
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * turnSpeed);
            player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);
            player.MoveRotation(targetRotation);
            moving = true;
            if(!animator.IsPlaying("walk")){
                animator.CrossFade("walk", 0.2f);
            }
        }else{
            moving = false;
            if(!animator.IsPlaying("idle")){
                animator.CrossFade("idle", 0.2f);
            }
        }
        if(stop){
            horizontalInput = 0;
            verticalInput = 0;
        }else{
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
    }
    IEnumerator exitRoom(){
        GameObject loadlevel = GameObject.Find("loadvar");
        fadeout.SetActive(true);
        yield return new WaitForSeconds(2f);
        loadlevel.GetComponent<AudioSource>().Stop();
        loadlevel.GetComponent<loadLevel>().called("grassyIsland");
    }
}
