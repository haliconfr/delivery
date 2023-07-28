using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fish : MonoBehaviour
{
    NavMeshAgent fishAI;
    public List<GameObject> positions;
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(startFish());
        fishAI = this.gameObject.GetComponent<NavMeshAgent>();
        StartCoroutine(goToPos());
    }
    IEnumerator goToPos(){
        fishAI.SetDestination(positions[Random.Range(0, positions.Count)].transform.position);
        yield return new WaitForSeconds(Random.Range(7, 12));
        StartCoroutine(goToPos());
    }
    IEnumerator startFish(){
        yield return new WaitForSeconds(Random.Range(0,5));
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}