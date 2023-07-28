using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setRaining : MonoBehaviour
{
    public GameObject rainSystem;
    public GameObject rainAmbience;
    public Material rainBox;
    public void setToRain()
    {
        Color32 color = new Color32(128, 128, 128, 255);
        RenderSettings.ambientLight = color;
        if(SceneManager.GetActiveScene().name == "companyIsland"){
            RenderSettings.fogDensity = 0.02f;
        }else{
            RenderSettings.fogDensity = 0.08f;
        }
        RenderSettings.ambientIntensity = 0;
        rainSystem.SetActive(true);
        rainAmbience.SetActive(true);
        RenderSettings.skybox = rainBox;
    }
}
