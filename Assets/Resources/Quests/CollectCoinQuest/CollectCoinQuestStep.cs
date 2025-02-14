using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinQuestStep : QuestStep
{
    public int coinCollected = 0;
    public int coinToComplete = 5;

    private void CoinCollected()
    {
        if(coinCollected < coinToComplete)
        {
            coinCollected += 1;
            UpdateState();
        }   

        if(coinCollected >= coinToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        //Task completion progress (e.g 2/5)
        string state = coinCollected.ToString();
        //Set state for quest step
        ChangeState(state);
    }

    //Get data in quest progress 
    protected override void SetQuestStepState(string state)
    {
        this.coinCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
