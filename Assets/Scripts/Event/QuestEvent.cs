using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{
    public event Action<string> onStartQuest;

    public void StartQuest(string id)
    {
        if(onStartQuest != null)
        {
            onStartQuest(id);
        }
    }

    public event Action<string> onAdvanceQuest;

    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;

    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<Quest> onQuestChangeState;

    public void QuestChangeState(Quest quest)
    {
        if (onQuestChangeState != null)
        {
            onQuestChangeState(quest);
        }
    }

    public event Action<string, int, QuestStepState> onQuestStateChangeState;

    public void QuestStepChangeState(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStateChangeState != null)
        {
            onQuestStateChangeState(id, stepIndex, questStepState);
        }
    }
}
