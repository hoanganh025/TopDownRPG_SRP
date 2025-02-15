using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeaponEvent
{
    public event Action onEquipped;
    public void Equipped()
    {
        if (onEquipped != null)
        {
            onEquipped();
        }
    }
}
