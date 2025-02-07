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
            GameEventManager.instance.levelEvent.LevelUp();
            currentLevel++;
            currentExp -= targetExp;
            targetExp += targetExp * expGrowthMultipler;
            Debug.Log(targetExp);

            //Stat growth when level up
            PlayerStat.instance.health += 2;
            PlayerStat.instance.attack += 1;
            
            //After growth, update stat ui
            PlayerStat.instance.UpdatePlayerStat();
        }
    }

    private void UpdateExpBar()
    {
        expBar.value = (float) currentExp / targetExp;
        levelText.text = ("LV " + currentLevel.ToString());
    }

}
