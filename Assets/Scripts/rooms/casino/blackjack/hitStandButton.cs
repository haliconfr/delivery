using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitStandButton : MonoBehaviour
{
    public GameObject eventSystem;
    public void hit()
    {
        eventSystem.GetComponent<blackjack>().StartCoroutine("playAnimHit");
    }
    public void stand()
    {
        eventSystem.GetComponent<blackjack>().StartCoroutine("playAnimStand");
    }
}