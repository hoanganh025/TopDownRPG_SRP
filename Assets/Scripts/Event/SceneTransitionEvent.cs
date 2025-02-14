using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionEvent
{
    public event Action<string, QuestState> onQuestSceneTransition;
    public void QuestSceneTransition(string id, QuestState state)
    {
        if (onQuestSceneTransition != null)
        {
            onQuestSceneTransition(id, state);
        }
    }
}
