using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvent
{
    public event Action<int> onExpGained;
    public void ExpGained(int _amountExp)
    {
        if(onExpGained != null)
        {
            onExpGained(_amountExp);
        }
    }

    public event Action onLevelUp;
    public void LevelUp()
    {
        if(onLevelUp != null)
        {
            onLevelUp();
        }
    }
}
