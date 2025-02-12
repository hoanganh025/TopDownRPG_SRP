using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKeyQuest : QuestStep
{
    public GameObject[] listKey;
    public int keyCollected;
    public int keyToComplete;

    private void OnEnable()
    {
        GameEventManager.instance.collectedQuestItemEvent.onCollectedQuestItem += CollectKey;
    }

    private void OnDisable()
    {
        GameEventManager.instance.collectedQuestItemEvent.onCollectedQuestItem -= CollectKey;
    }

    private void Start()
    {
        keyToComplete = listKey.Length;
    }

    private void CollectKey(GameObject key)
    {
        for(int i = 0; i < listKey.Length; i++)
        {
            if(listKey[i].name == key.name)
            {
                keyCollected += 1;
                UpdateState();
            }
        }

        if (keyCollected >= keyToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        //Task completion progress (e.g 2/5)
        string state = keyCollected.ToString();
        Debug.Log("Collected key = "+state);
        //Set state for quest step
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.keyCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
