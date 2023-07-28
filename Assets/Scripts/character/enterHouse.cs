using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterHouse : MonoBehaviour
{
    public float sightRange;
    public LayerMask doors;
    public GameObject fadeout, loadlevel, box, skip, canvas, creak;
    void Start()
    {
        loadlevel = GameObject.Find("loadvar");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, sightRange, doors))
                {
                    if(hit.collider.gameObject.name == "cafe"){
                    creak.SetActive(true);
                    loadlevel.GetComponent<AudioSource>().Stop();
                    box.SetActive(false);
                    skip.SetActive(false);
                    canvas.SetActive(true);
                    fadeout.SetActive(true);
                    StartCoroutine(load());
                }
            }
        }
    }
    IEnumerator load(){
        yield return new WaitForSeconds(1.5f);
        loadlevel.GetComponent<loadLevel>().called("cafe");
    }
}