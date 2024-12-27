using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int health, attack, defense, agility, mana, AP;
    [SerializeField] private TMP_Text healthText, attackText, defenseText, agilityText, manaText, APText;

    private void Start()
    {
        UpdatePlayerStat();
    }

    //Update player stats when equip/unequip gear
    public void UpdatePlayerStat()
    {
        healthText.text = health.ToString();
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        agilityText.text = agility.ToString();
        manaText.text = mana.ToString();
        APText.text = AP.ToString();
    }
}
