using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody player;
    public Animator animator;
    public GameObject mc, fadeout;
    public Transform cam;
    public float speed, turnSpeed, thickness;
    public LayerMask npcs, doors;
    public float sightRange;
    public bool stop, moving;
    float horizontalInput, verticalInput;
    public GameObject cafepoint;
    bool planedoor;
    void Start()
    {
        player = gameObject.GetComponent<Rigidbody>();
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
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("walk")){
                animator.SetBool("gotowalk", true);
            }else{
                animator.SetBool("gotowalk", false);
            }
        }else{
            moving = false;
            if(!animator.GetCurrentAnimatorStateInfo(0).IsName("idle")){
                animator.SetBool("gotoidle", true);
            }else{
                animator.SetBool("gotoidle", false);
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
    IEnumerator gotoCasino(){
        yield return new WaitForSeconds(0.6f);
        fadeout.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("casino lobby");
    }
    IEnumerator grade(){
        fadeout.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("grade");
    }
    void OnTriggerEnter(Collider collider){
        if(collider.name == "Cube.014"){
            planedoor = true;
        }
    }
    void OnTriggerExit(Collider collider){
        if(collider.name == "Cube.014"){
            planedoor = false;
        }
    }
    void Update(){
        if(planedoor){
            if(Input.GetKeyDown(KeyCode.E)){
                if(!stop){
                    fadeout.SetActive(true);
                    StartCoroutine(grade());
                }
            }
        }
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, thickness, transform.TransformDirection(Vector3.forward), out hit, sightRange, doors))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(!stop){
                    if(hit.collider.name == "casino"){
                        hit.collider.transform.parent.GetComponent<Animator>().Play("openDoors");
                        StartCoroutine(gotoCasino());
                    }
                }
            }
        }
        RaycastHit hit2;
        if (Physics.SphereCast(transform.position, thickness, transform.TransformDirection(Vector3.forward), out hit2, sightRange, npcs))
        {
            if(Input.GetKeyDown(KeyCode.E)){
                if(!stop){
                    hit2.collider.gameObject.GetComponent<npcs>().dialogue();
                }
            }
        }
    }
}