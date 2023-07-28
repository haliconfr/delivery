using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class dropOffPackage : MonoBehaviour
{
    public GameObject package;
    GameObject loadlevel;
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "dropoff"){
            package.SetActive(true);
            loadlevel = GameObject.Find("loadvar");
            loadlevel.GetComponent<loadLevel>().dropped = true;
            string save = Application.dataPath + "/game.sg";
            string linesSave = File.ReadAllText(save).ToString();
            string[] linesArray = linesSave.Split('\n');
            linesArray[2] = (int.Parse(linesArray[2]) + 1).ToString();
            File.WriteAllLines(save, linesArray);
            other.gameObject.SetActive(false);
        }
    }
}
