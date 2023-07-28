using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepSounds : MonoBehaviour
{
    public AudioSource grassStep, grassStep2;
    void hitGround()
    {
        grassStep.Play();
    }
        void hitGroundAgain()
    {
        grassStep2.Play();
    }
}
