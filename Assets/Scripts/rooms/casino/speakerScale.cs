using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakerScale : MonoBehaviour
{
    public AudioSource audioSource;
    public float update;
    public int sampleDataLength;
    float currentUpdate;
    public float clipLoudness;
    float[] clipSampleData;
    public float sizeFactor;
    public float minSize;
    public float maxSize;
    void Start()
    {
        clipSampleData = new float[sampleDataLength];
    }
    void Update()
    {
        currentUpdate += Time.deltaTime;
        if(currentUpdate >= update){
            currentUpdate = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            clipLoudness = 0f;
            foreach(var sample in clipSampleData){
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength;
            clipLoudness *= sizeFactor;
            clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            this.gameObject.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
        }
        
    }
}
