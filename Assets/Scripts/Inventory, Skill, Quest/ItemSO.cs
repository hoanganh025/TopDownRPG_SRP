using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObject/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [SerializeField] private Sprite itemSprite;
    //[SerializeField] private bool haveDuration;
    [SerializeField] private float Heal, Mana, Ability;
    [TextArea][SerializeField] private string Description;
    public ItemType itemType;

    public bool UseItem()
    {
        if(Heal != 0 && itemType == ItemType.comsumable)
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

        if (Mana != 0 && itemType == ItemType.comsumable)
        {
            PlayerMana playerMana = GameObject.Find("PlayerMana").GetComponent<PlayerMana>();
            //If player mana is full, item mana can't be used
            if (playerMana.currentMana == PlayerStat.instance.mana)
            {
                return false;
            }
            else
            {
                playerMana.RecoveryMana(Mana);
                return true;
            }
        }
        return false;
    }
}
