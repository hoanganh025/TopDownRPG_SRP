using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] public string itemName;
    [SerializeField] private Sprite itemSprite;
    //[SerializeField] private bool haveDuration;
    [SerializeField] private float Heal, Mana, Ability;
    [TextArea][SerializeField] private string Description;
    [SerializeField] private ItemType itemType;

    public bool UseItem()
    {
        if(Heal != 0)
        {
            PlayerHeal playerHeal = GameObject.Find("PlayerHeal").GetComponent<PlayerHeal>();
            //If player heal is full, item heal can't be used
            if (playerHeal.currentHealth == PlayerStat.instance.health)
            {
                return false;
            }
            else
            {
                playerHeal.Healing(Heal);
                return true;
            }
        }
        return false;
    }
}
