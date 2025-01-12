using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "ScriptableObject/EquipmentSO")]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int health, attack, defense, agility, mana, AP;

    //When equip gear, plus item's parameter to player stats
    public void EquipGear()
    {
        PlayerStat playerStat = GameObject.Find("StatPanel").GetComponent<PlayerStat>();
        playerStat.health += this.health;
        playerStat.attack += this.attack;
        playerStat.defense += this.defense;
        playerStat.agility += this.agility;
        playerStat.mana += this.mana;
        playerStat.AP += this.AP;

        playerStat.UpdatePlayerStat();
    }

    //When equip gear, minus item's parameter to player stats
    public void UnEquipGear()
    {
        PlayerStat playerStat = GameObject.Find("StatPanel").GetComponent<PlayerStat>();
        playerStat.health -= this.health;
        playerStat.attack -= this.attack;
        playerStat.defense -= this.defense;
        playerStat.agility -= this.agility;
        playerStat.mana -= this.mana;
        playerStat.AP -= this.AP;

        playerStat.UpdatePlayerStat();
    }
}
