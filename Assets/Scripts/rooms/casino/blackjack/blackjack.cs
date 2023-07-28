using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static bl;
public class blackjack : MonoBehaviour
{
    public List<GameObject> cards;
    public GameObject card, playerCard, flippedCard, maddie, player, textBox;
    public GameObject cardholder;
    public GameObject playerHand, dealerHand;
    public List<GameObject> dealerCards = new List<GameObject>();
    //public int playerHandInt, dealerHandInt;
    //int tempHand;
    //int cardCount, dealCount;
    public Vector3 secondMove, thirdMove, fourthMove, fifthMove, sixthMove;
    public TMPro.TMP_Text count;
    float slerpTime;

    void Start()
    {
        foreach(Transform child in cardholder.transform){
            cards.Add(child.gameObject);
        }
        hit(Turn.Computer, true);
        hit(Turn.Computer, true);
        hit(Turn.Player);
        hit(Turn.Player);
    }
    bool hit(Turn t, bool faceDown = false)
    {
        GameObject card = cards[Random.Range(0, cards.Count)];
        card.transform.parent = null;
        switch(t)   {
            case Turn.Computer:
                dealerCards.Add(card);
                switch (bl.CardCount(t))  {
                    case 0:
                        card.transform.position = dealerHand.transform.position;
                    break;
                    case 1: 
                        card.transform.position = new Vector3(dealerHand.transform.position.x, dealerHand.transform.position.y, thirdMove.z);
                    break;
                    case 2:
                        card.transform.position = new Vector3(dealerHand.transform.position.x, dealerHand.transform.position.y, secondMove.z);
                    break;
                    case 3:
                        card.transform.position = new Vector3(4.71f, dealerHand.transform.position.y, fourthMove.z);
                    break;
                }
            break;
            case Turn.Player:
                switch (bl.CardCount(t))  {
                    case 0:
                        card.transform.position = playerHand.transform.position;
                    break;
                    case 1:
                        card.transform.position = secondMove;
                    break;
                    case 2: 
                        card.transform.position = thirdMove;
                    break;
                    case 3:
                        card.transform.position = fourthMove;
                    break;
                    case 4:
                        card.transform.position = fifthMove;
                    break;
                }
            break;
        }
        card.transform.Rotate(0, faceDown?0:180, 0);
        cards.Remove(card);

        if(card.name.Contains("Ace")){
            switch(t)   {
                case Turn.Computer:
                    bl.Add(t,bl.computerScore+11>21?1:11);
                break;
                case Turn.Player:
                    bl.Add(t,bl.playerScore+11>21?1:11);
                break;
            }
        }
        if(card.name.Contains("Two")){
            bl.Add(t,2);
        }
        if(card.name.Contains("Three")){
            bl.Add(t, 3);
        }
        if(card.name.Contains("Four")){
            bl.Add(t, 4);
        }
        if(card.name.Contains("Five")){
            bl.Add(t, 5);
        }
        if(card.name.Contains("Six")){
            bl.Add(t, 6);
        }
        if(card.name.Contains("Seven")){
            bl.Add(t, 7);
        }
        if(card.name.Contains("Eight")){
            bl.Add(t, 8);
        }
        if(card.name.Contains("Nine")){
            bl.Add(t, 9);
        }
        if(card.name.Contains("Ten")){
            bl.Add(t, 10);
        }
        if(card.name.Contains("King")){
            bl.Add(t, 10);
        }
        if(card.name.Contains("Queen")){
            bl.Add(t, 10);
        }
        if(card.name.Contains("Jack")){
            bl.Add(t, 10);
        }

        var bust = bl.Score(t)>21;
        if (bust) {
            switch(t)   {
                case Turn.Computer:
                    Finish(false);
                break;
                case Turn.Player:
                    Finish(true);
                break;
            }
        }
        return bust;
    }

    private void Finish(bool lost) {
        textBox.GetComponent<endingDialogue>().lost = lost;
        textBox.SetActive(true);
        Destroy(this.gameObject);
    }


    public IEnumerator playAnimStand(){
        //turn the face down cards back up
        foreach(GameObject child in dealerCards){
            child.transform.Rotate(0,180,0);
        }

        while(bl.computerScore<=17)  {
            maddie.GetComponent<Animator>().CrossFade("use cards", 0.3f);
            yield return new WaitForSeconds(0.55f);
            if (hit(Turn.Computer))
                yield break;
        }
        if (bl.computerScore>=bl.playerScore)
        {
            Finish(true);
        }
        else {
            Finish(false);
        }
    }
    public IEnumerator playAnimHit(){
        player.GetComponent<Animator>().CrossFade("use cards", 0.3f);
        yield return new WaitForSeconds(0.52f);
        hit(Turn.Player);
    }
    void Update(){
        count.text = bl.computerScore.ToString();
    }
}


public static class bl
{
   public static int computerScore = 0;
   public static int computerCards = 0;
   public static int playerScore = 0;
   public static int playerCards = 0;

   public enum Turn {Computer, Player};


   public static void Add(Turn t, int score) {
       switch(t) {
           case Turn.Computer:
            computerScore+=score;
            computerCards++;
           break;
           case Turn.Player:
            playerScore+=score;
            playerCards++;
           break;
       }
   }

    public static int CardCount(Turn t){
        return t == Turn.Computer?computerCards:playerCards;
    }
   public static int Score(Turn t){
       return t == Turn.Computer?computerScore:playerScore;
   }

}
