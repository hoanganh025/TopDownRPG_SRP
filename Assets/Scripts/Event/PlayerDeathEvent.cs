using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEvent
{
    public event Action onPlayerDeath;
    public void PlayerDeath()
    {
       if (onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }
}
