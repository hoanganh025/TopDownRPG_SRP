using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState questState;
    public int currentQuestStepIndex;
    private QuestStepState[] questStepStates;

    //Constructor get quest info from QuestInfoSO and create new quest
    public Quest(QuestInfoSO questInfo)
    {
        //Get info quest from questInfo
        info = questInfo;
        //Set state is requiment not met
        questState = QuestState.REQUIMENT_NOT_MET;
        //Set current state is 0
        currentQuestStepIndex = 0;

        //This array length of quest step state equal length of questStepPrefabs
        //If questStep has more state than questStepPrefabs? Change this when pj need
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length]; 
        for (int i = 0; i < questStepStates.Length; i++)
        {
            //Each element in questStepStates is a new QuestStepState
            questStepStates[i] = new QuestStepState();
        }
    }

    //Constructor get questInfo from save system (have currentQuestStepIndex and array questStepStates)
    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.questState = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        if(this.questStepStates.Length != this.info.questStepPrefabs.Length)
        {
            Debug.LogWarning("Quest step prefab and quest state step are difference of length" + this.info.id);
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        //Check current quest step exist (currentQuestStepIndex start with 0, length in array start with 1)
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform currentTranform)
    {
        //if current quest step exist, get it 
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            //If not exsist quest step prefab
            //Instantiate prefab questStep
             QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, currentTranform)
                .GetComponent<QuestStep>();

            //Add info to questStep 
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if(CurrentStepExists())
        {
            //If current quest step is exist, get it from QuestInfoSO
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogWarning("no current quest step: Questid = " + info.id + ", step index = " + currentQuestStepIndex);
        }

        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        //Check quest index is within the range 
        if(stepIndex < questStepStates.Length) //(e.g 0<1)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("Trial to access quest step data, but stepIndex out of range, " +
                "Quest Id: "+info.id+", Step index:"+stepIndex);
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(questState, currentQuestStepIndex, questStepStates);
    }
}
