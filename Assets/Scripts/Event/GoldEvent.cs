using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEvent
{
    public event Action<int> onGoldGained;
    public void GoldGained(int _amount)
    {
        if (onGoldGained != null)
        {
            onGoldGained(_amount);
        }
    }

    public event Action<int> onGoldChange;
    public void GoldChange(int _amount)
    {
        if(onGoldChange != null)
        {
            onGoldChange(_amount);
        }
    }
}
