using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepObject : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
