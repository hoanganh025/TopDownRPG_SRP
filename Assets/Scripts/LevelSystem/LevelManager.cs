using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private float targetExp;
    [SerializeField] private float expGrowthMultipler = 1.2f;

    public int currentLevel;
    public float currentExp;

    [SerializeField] private Slider expBar;

    private void OnEnable()
    {
        GameEventManager.instance.levelEvent.onExpGained += IncreaseExp;
    }

    private void OnDisable()
    {
        GameEventManager.instance.levelEvent.onExpGained -= IncreaseExp;
    }

    private void Start()
    {
        currentLevel = 1;
        UpdateExpBar();
    }

    private void IncreaseExp(int _amount)
    {
        currentExp += _amount;
        CheckLevelUp();
        UpdateExpBar();
    }

    private void CheckLevelUp()
    {
        while (currentExp >= targetExp)
        {
            currentLevel++;
            currentExp -= targetExp;
            targetExp += targetExp * expGrowthMultipler;

            //Stat growth when level up
            PlayerStat.instance.health += 2;
            PlayerStat.instance.defense += 1;
            PlayerStat.instance.attack += 1;
            PlayerStat.instance.mana += 5;
            PlayerStat.instance.agility += 0.2f;
            PlayerStat.instance.AP += 3f;
            
            //After growth, update stat ui
            PlayerStat.instance.UpdatePlayerStat();
            //Fill heal and mana bar after level up
            GameEventManager.instance.levelEvent.LevelUp();
        }
    }

    private void UpdateExpBar()
    {
        expBar.value = (float) currentExp / targetExp;
        levelText.text = ("LV " + currentLevel.ToString());
    }

}
