using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class betting : MonoBehaviour
{
    public TMP_Text number;
    int moneyInt;
    public int currentMoney;
    public AudioSource error;
    public int balance;
    public GameObject balanceGet;
    void Start(){
        balance = balanceGet.GetComponent<currentBalance>().savedBalance;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void buttonIncrease()
    {
        currentMoney = int.Parse(number.text.Replace("$ ", ""));
        moneyInt = moneyInt + 50;
        if(balance < moneyInt){
            moneyInt = balance;
            error.Play();
        }
        StartCoroutine(increase());
    }
    public void buttonDecrease()
    {
        currentMoney = int.Parse(number.text.Replace("$ ", ""));
        moneyInt = moneyInt - 50;
        if(moneyInt < 0){
            moneyInt = 0;
            error.Play();
        }
        StartCoroutine(decrease());
    }
    IEnumerator increase(){
        yield return new WaitForSeconds(0.01f);
        if(currentMoney != moneyInt){
            currentMoney++;
            number.text = "$ " + currentMoney.ToString();
            StartCoroutine(increase());
        }
    }
        IEnumerator decrease(){
        yield return new WaitForSeconds(0.01f);
        if(currentMoney != moneyInt){
            currentMoney--;
            number.text = "$ " + currentMoney.ToString();
            StartCoroutine(decrease());
        }
    }
}
