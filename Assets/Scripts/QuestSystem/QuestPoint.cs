using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;
    [SerializeField] private DialogTrigger dialogTrigger;

    [Header("Config")]
    [SerializeField] private bool startPoint = false;
    [SerializeField] private bool finishPoint = false;

    public BoxCollider2D bossRoomDoorCollider;
    public string questID;
    public QuestState currentQuestState;

    private bool playerIsNear = false;
    private QuestIcon questIcon;

    private void Awake()
    {
        questID = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestChangeState += QuestStateChange;
        GameEventManager.instance.inputEvent.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestChangeState -= QuestStateChange;
        GameEventManager.instance.inputEvent.onSubmitPressed -= SubmitPressed;
    }

    //When pressed button Q
    private void SubmitPressed()
    {
        if(!playerIsNear)
        {
            return;
        }

        dialogTrigger.TriggerDialog();

        //Start or finish quest
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            //Call StartQuest from QuestManager
            GameEventManager.instance.questEvent.StartQuest(questID);
            Debug.Log("Nhan nhiem vu");
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            //Call FinishQuest from QuestManager
            GameEventManager.instance.questEvent.FinishQuest(questID);
            //If finish quest collect key, open boss room
            if (bossRoomDoorCollider != null)
            {
                bossRoomDoorCollider.isTrigger = true;
            }
            Debug.Log("Tra nhiem vu");
        }
    }

    private void QuestStateChange(Quest quest)
    {
        //Only update the quest state if this point has the corresponding quest 
        if(quest.info.id.Equals(questID))
        {
            //Set quest point state equal quest state
            currentQuestState = quest.questState;
            //Change icon quest
            questIcon.SetStateIcon(currentQuestState, startPoint, finishPoint);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
