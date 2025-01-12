using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if(questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    protected void FinishQuestStep()
    {
        if(!isFinished)
        {
            isFinished = true;

            //TODO - advance the quest forward now tha we're finished this step
            GameEventManager.instance.questEvent.AdvanceQuest(questId);

            Destroy(gameObject);
        }
    }

    //Change state of quest step
    protected void ChangeState(string newState)
    {
        //(e.g Quest 1, step 1, new QuestStepState 2 (2/5))
        GameEventManager.instance.questEvent.QuestStepChangeState(questId, stepIndex, new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);
}
