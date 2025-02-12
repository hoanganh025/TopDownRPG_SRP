using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedQuestItemEvent
{
    public event Action<GameObject> onCollectedQuestItem;

    public void CollectedQuestItem(GameObject item)
    {
        if (onCollectedQuestItem != null)
        {
            onCollectedQuestItem(item);
        }
    }
}
