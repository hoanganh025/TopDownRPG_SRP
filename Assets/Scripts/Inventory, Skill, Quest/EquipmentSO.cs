using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "ScriptableObject/EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int health, attack, defense, agility, mana, AP;

    public ItemType itemType;

    //When equip gear, plus item's parameter to player stats
    public void EquipGear()
    {
        PlayerStat.instance.health += this.health;
        PlayerStat.instance.attack += this.attack;
        PlayerStat.instance.defense += this.defense;
        PlayerStat.instance.agility += this.agility;
        PlayerStat.instance.mana += this.mana;
        PlayerStat.instance.AP += this.AP;

        PlayerStat.instance.UpdatePlayerStat();
    }

    //When equip gear, minus item's parameter to player stats
    public void UnEquipGear()
    {
        PlayerStat.instance.health -= this.health;
        PlayerStat.instance.attack -= this.attack;
        PlayerStat.instance.defense -= this.defense;
        PlayerStat.instance.agility -= this.agility;
        PlayerStat.instance.mana -= this.mana;
        PlayerStat.instance.AP -= this.AP;

        PlayerStat.instance.UpdatePlayerStat();
    }
}
