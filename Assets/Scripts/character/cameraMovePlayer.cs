using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovePlayer : MonoBehaviour
{
    public Transform player;
    public float camDamp;
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y + 3.65f, player.position.z - 3.73f), camDamp);
    }
}