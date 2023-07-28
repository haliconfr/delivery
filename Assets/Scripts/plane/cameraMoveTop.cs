using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMoveTop : MonoBehaviour
{
    public float damp, camDamp, camTurnDamp, speed;
    public Transform mc, planeModel, armature;
    public GameObject characterAnim, character, characterCam, camswitch, landkey;
    bool landing, played;
    void FixedUpdate()
    {
        playAnims();
        planeControl();
        mc.transform.position += mc.transform.forward * speed * Time.deltaTime;
        if(speed <= 0.5f){
            if(!played){
                StartCoroutine(characterExit());
            }
        }
    }
    void Update()
    {
        if(!landing){
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                landing = true;
                landkey.SetActive(false);
                StartCoroutine(speedDiv());
                planeModel.GetComponent<Animation>().Play("landing");
            }
        }
    }
    void planeControl(){
        if(Input.GetKey(KeyCode.A)){
            mc.rotation = Quaternion.Slerp(new Quaternion(0, mc.transform.rotation.y, mc.transform.rotation.z, mc.transform.rotation.w), new Quaternion(0, mc.transform.rotation.y - 0.5f, mc.transform.rotation.z + 0.1f, mc.transform.rotation.w), damp);
        }
        if(Input.GetKeyUp(KeyCode.A)){
            mc.rotation = Quaternion.Slerp(new Quaternion(0, mc.transform.rotation.y, mc.transform.rotation.z, mc.transform.rotation.w), new Quaternion(0, mc.transform.rotation.y, 0, mc.transform.rotation.w), damp);
        }
        if(Input.GetKey(KeyCode.D)){
            mc.rotation = Quaternion.Slerp(new Quaternion(0, mc.transform.rotation.y, mc.transform.rotation.z, mc.transform.rotation.w), new Quaternion(0, mc.transform.rotation.y + 0.5f, mc.transform.rotation.z - 0.1f, mc.transform.rotation.w), damp);
        }else{
            mc.rotation = Quaternion.Slerp(new Quaternion(0, mc.transform.rotation.y, mc.transform.rotation.z, mc.transform.rotation.w), new Quaternion(0, mc.transform.rotation.y, 0, mc.transform.rotation.w), damp);
        }
        if(landing != true){
            mc.position = new Vector3(mc.position.x, 6.749036f, mc.position.z);
        }
    }
    IEnumerator speedDiv(){
        speed = Mathf.Lerp(speed, speed/4, 0.2f);
        damp = Mathf.Lerp(damp, damp/4, 0.2f);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(speedDiv());
    }
    void playAnims(){
        if(landing != true){
            if(!planeModel.GetComponent<Animation>().isPlaying){
                planeModel.GetComponent<Animation>().Play("propellerspin");
            }
        }
    }
    IEnumerator characterExit(){
        played = true;
        planeModel.gameObject.GetComponent<Animation>().Play("planedooropen");
        yield return new WaitForSeconds(1.25f);
        characterAnim.SetActive(true);
        characterAnim.GetComponent<Animation>().Play("exitplane");
        yield return new WaitForSeconds(2f);
        character.SetActive(true);
        character.transform.position = armature.transform.position;
        character.transform.rotation = new Quaternion(characterAnim.transform.rotation.x, characterAnim.transform.rotation.y- 1.5708f, characterAnim.transform.rotation.z, characterAnim.transform.rotation.w);
        characterAnim.SetActive(false);
        camswitch.GetComponent<CinemachineSwitcherOV>().player();
    }
    public void autoLand(){
        landing = true;
        landkey.SetActive(false);
        StartCoroutine(speedDiv());
        planeModel.GetComponent<Animation>().Play("landing");
    }
}
