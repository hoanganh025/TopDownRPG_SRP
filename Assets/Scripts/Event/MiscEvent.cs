using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvent
{
    public event Action onItemCollected;
    public void ItemCollected()
    {
       if (onItemCollected != null)
        {
            onItemCollected();
        }
    }
}
