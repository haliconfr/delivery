using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeparticles : MonoBehaviour
{
    public GameObject part, part1, part2;
    List<GameObject> particles;
    Vector3 reduction = new Vector3(.01f, .01f, .01f);
    bool going;
    void Start()
    {
        particles = new List<GameObject>();
        particles.Add(part);
        particles.Add(part1);
        particles.Add(part2);
        StartCoroutine(chooseParts());
    }

    IEnumerator chooseParts()
    {
        going = true;
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        GameObject dupe = GameObject.Instantiate(particles[Random.Range(0, particles.Count)]);
        dupe.transform.position = transform.position;
        dupe.transform.rotation = new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        StartCoroutine(spinPart(dupe));
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        GameObject dupe2 = GameObject.Instantiate(particles[Random.Range(0, particles.Count)]);
        dupe2.transform.position = transform.position;
        dupe2.transform.rotation = new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        StartCoroutine(spinPart(dupe2));
        going = false;
    }
    IEnumerator spinPart(GameObject dupe)
    {
        if(dupe.transform.localScale.x > 0)
        {
            dupe.transform.localScale = dupe.transform.localScale - reduction;
            dupe.transform.rotation = new Quaternion(dupe.transform.rotation.x - (0.15f), dupe.transform.rotation.y - (0.2f), dupe.transform.rotation.z - (0.1f), dupe.transform.rotation.w);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(spinPart(dupe));
        }
        else
        {
            Destroy(dupe);
            if (!going)
            {
                StartCoroutine(chooseParts());
            }
        }
    }
}
