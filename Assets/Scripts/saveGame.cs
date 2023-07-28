using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class saveGame : MonoBehaviour
{
    string[] linesArray;
    public TMP_Text grade;
    public TMP_Text points;
    float totalpoints;
    int currentNumber;
    int tasksToPoints;
    public Slider slider;
    public GameObject increase, finish;
    int task;
    public GameObject tick;
    void OnEnable()
    {
        string save = Application.dataPath + "/game.sg";
        Debug.Log(save);
        linesArray = File.ReadAllLines(save);
        task = int.Parse(linesArray[2]);
        tasksToPoints = task*100;
        linesArray[1] = (int.Parse(linesArray[1]) + tasksToPoints).ToString();
        totalpoints = int.Parse(linesArray[1]);
        linesArray[2] = "0";
        File.WriteAllLines(save, linesArray);
        gradeSelect();
        tick.SetActive(true);
        StartCoroutine(pointsIncrease());
    }
    IEnumerator pointsIncrease(){
        if(currentNumber < (task*100)){
            Debug.Log("inc");
            currentNumber = int.Parse(points.text.Replace("Points: ", ""));
            currentNumber++; 
            points.text = "Points: " + currentNumber.ToString();
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(pointsIncrease());
        }else{
            tick.SetActive(false);
            points.text = "Points: " + tasksToPoints.ToString();
            yield return new WaitForSeconds(2f);
            StartCoroutine(sliderIncrease());
            increase.SetActive(true);
        }
    }
    IEnumerator sliderIncrease(){
        if(slider.value <= totalpoints/25){
            slider.value = Mathf.MoveTowards(slider.value, totalpoints/25, 100f * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(sliderIncrease());
        }
        if(slider.value == totalpoints/25){
            increase.SetActive(false);
            finish.SetActive(true);
        }
    }
    void gradeSelect(){
        if(task == 4){
            grade.text = "Grade: A+";
        }
        if(task == 3){
            grade.text = "Grade: A";
        }
        if(task == 2){
            grade.text = "Grade: B";
        }
        if(task == 1){
            grade.text = "Grade: C";
        }
        if(task == 0){
            grade.text = "Grade: F";
        }
    }
}
