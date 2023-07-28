using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class currentBalance : MonoBehaviour
{
    public int savedBalance;
    public TMP_Text text, textBalance;
    public int moneyBet;
    string balance;
    void Start()
    {
        string save = Application.dataPath + "/game.sg";
        Debug.Log(save);
        if(File.Exists(save)){
            using (StreamReader sw = new StreamReader(save))
            {
                savedBalance = int.Parse(sw.ReadLine());
                sw.Close();
            }
        }else{
            File.Create(save).Dispose();
            using (StreamWriter sw = new StreamWriter(save))
            {
                sw.WriteLine("150");
                savedBalance = 150;
            }
        }
        text.text = "balance: " + savedBalance.ToString();
    }
    void Update(){
        moneyBet = this.gameObject.GetComponent<betting>().currentMoney;
        this.gameObject.GetComponent<betting>().balance = savedBalance;
        text.text = "balance: " + (savedBalance - moneyBet).ToString();
        if(Input.GetKeyDown(KeyCode.Return)){
            GameObject.Find("loadvar").GetComponent<loadLevel>().called("blackjack");
            betAmount.amount = int.Parse(textBalance.text.Replace("$ ", ""));
            betAmount.wallet = int.Parse(text.text.Replace("balance: ", ""));
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
