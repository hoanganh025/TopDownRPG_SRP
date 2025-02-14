using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestSate = true;

    [SerializeField] private LevelManager levelManager;
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onStartQuest += StartQuest;
        GameEventManager.instance.questEvent.onAdvanceQuest += AdvanceQuest;
        GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;

        GameEventManager.instance.questEvent.onQuestStateChangeState += QuestStepStateChange;
        GameEventManager.instance.sceneTransitionEvent.onQuestSceneTransition += ChangeQuestState;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onStartQuest -= StartQuest;
        GameEventManager.instance.questEvent.onAdvanceQuest -= AdvanceQuest;
        GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;

        GameEventManager.instance.questEvent.onQuestStateChangeState -= QuestStepStateChange;
        GameEventManager.instance.sceneTransitionEvent.onQuestSceneTransition -= ChangeQuestState;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            //Initialized In_progress quest step 
            if(quest.questState == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            //Update questpoint and questicon for all quest
            GameEventManager.instance.questEvent.QuestChangeState(quest);
        }
    }

    private void Update()
    {
        //Loop through all quest
        foreach(Quest quest in questMap.Values)
        {
            //Check can get quest 
            if(quest.questState == QuestState.REQUIMENT_NOT_MET && CheckCanGetRequiment(quest))
            {
                //If we're now meeting and get the requiment, switch over to CAN_START state
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    public void ChangeQuestState(string id, QuestState state)
    {
        //Get quest by id
        Quest quest = GetQuestByID(id);
        //Set state
        quest.questState = state;
        //Change state of QuestPoint and QuestIcon
        GameEventManager.instance.questEvent.QuestChangeState(quest);
    }

    private bool CheckCanGetRequiment(Quest _quest)
    {
        //Start true and prove to be false
        bool getRequiment = true;

        //Check player requiment
        if(levelManager.currentLevel < _quest.info.levelRequiment)
        {
            getRequiment = false;
        }

        //Check quest prerequisite for completion
        foreach(QuestInfoSO prequisiteQuestInfo in _quest.info.questPrerequisites)
        {
            if(GetQuestByID(prequisiteQuestInfo.id).questState != QuestState.FINISHED)
            {
                getRequiment = false;
                break;
            }
        }

        return getRequiment;
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        //Create queststep from prefab
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);

        //Move on next step (plus questStepIndex)
        quest.MoveToNextStep();

        //If exsist questStep, instantiate it
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        //If there are no more steps, the we're finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimReward(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimReward(Quest quest)
    {
        GameEventManager.instance.levelEvent.ExpGained(quest.info.expReward);
        GameEventManager.instance.goldEvent.GoldGained(quest.info.goldReward);
    }

    //Quest step state change (string) 
    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);

        quest.StoreQuestStepState(questStepState, stepIndex); //(e.g 2, 0)
        ChangeQuestState(id, quest.questState);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Load all QuestInfoSo under the Asssets/Resources/Quests folder. Can be user drag and drop in inspector
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        //Create quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate id when create quest map in id = "+questInfo.id);
            }
            //Add each info of quest to quest map if quest not exsist 
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    //Get quest by ID
    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("Not find the quest with id = " + id);
        }
        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach(Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            //serialize using JsonUtility, but use what ever you want here (like json.net)
            string serializedData = JsonUtility.ToJson(questData);
            //Saving to playerPref just a quick example for this tutorial video
            //you probably don't want to save this info there long term
            //instead, use an actual save&load system and write the file, clould, ...
            PlayerPrefs.SetString(quest.info.id, serializedData);
            Debug.Log(serializedData);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save quest "+quest.info.id + e);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            //Load quest from save data
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestSate == true)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            //otherwise, initialize new quest
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load quest with id" + questInfo.id + ":" + e);
        }
        return quest;
    }
}
