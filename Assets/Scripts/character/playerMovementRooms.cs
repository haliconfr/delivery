using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementRooms : MonoBehaviour
{
    Rigidbody player;
    Animation animator;
    public GameObject mc, npcName, fadeout;
    public bool moving;
    public float speed, turnSpeed, thickness;
    public LayerMask npcs, doors;
    public float sightRange, sightRangeNPC;
    public bool stop, inTrigger;
    float horizontalInput, verticalInput;
    void Start()
    {
        player = gameObject.GetComponent<Rigidbody>();
        animator = mc.GetComponent<Animation>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "exit"){
            inTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "exit"){
            inTrigger = false;
        }
    }
    void FixedUpdate()
    {
        if(inTrigger){
            if(Input.GetKeyDown(KeyCode.E)){
                GameObject loadlevel = GameObject.Find("loadvar");
                loadlevel.GetComponent<loadLevel>().called("grassyIsland");
            }
        }
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
    void Update(){
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, thickness, transform.TransformDirection(Vector3.forward), out hit, sightRangeNPC, npcs))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                npcName.GetComponent<currentNpc>().npc = hit.collider.gameObject.name;
                hit.collider.gameObject.GetComponent<npcsRoom>().Invoke("dialogue", 0f);
            }
        }

    }
}