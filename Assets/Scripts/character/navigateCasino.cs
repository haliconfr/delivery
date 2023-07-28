using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navigateCasino : MonoBehaviour
{
    public GameObject fadeout, lobby, hall, mc;
    public Vector3 pos;
    public LayerMask npc;
    void Start(){
        //GameObject.Find("music").GetComponent<AudioSource>().Pause();
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "to hall"){
            StartCoroutine(halls());
        }
        if(other.gameObject.name == "to lobby"){
            StartCoroutine(lobbystart());
        }
        if(other.gameObject.name == "to blackjack"){
            StartCoroutine(blackjackstart());
        }
        if(other.gameObject.name == "exit"){
            StartCoroutine(exit());
        }
    } 
    IEnumerator halls()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mc.transform.localRotation = new Quaternion(mc.transform.rotation.x, 0, mc.transform.rotation.z, mc.transform.rotation.w);
        mc.transform.localPosition = pos;
        fadeout.SetActive(false);
        hall.SetActive(true);
        lobby.SetActive(false);
    }
    IEnumerator lobbystart()
    {
        fadeout.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mc.transform.localRotation = new Quaternion(mc.transform.rotation.x, 180, mc.transform.rotation.z, mc.transform.rotation.w);
        mc.transform.localPosition = pos;
        fadeout.SetActive(false);
        hall.SetActive(false);
        lobby.SetActive(true);
    }
    IEnumerator blackjackstart(){
        fadeout.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("betting");
    }
    IEnumerator exit(){
        fadeout.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        GameObject.Find("loadvar").GetComponent<loadLevel>().called("grassyIsland");
    }
    void Update(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4f, npc)){
            if(Input.GetKeyDown(KeyCode.E)){
                hit.collider.gameObject.GetComponent<npcCasino>().dialogue();
            }
        }
    }
}
