using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private int targetExp;
    [SerializeField] private int increaseNextLvExp;

    public int currentLevel;
    public int currentExp;

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
            targetExp += increaseNextLvExp;
        }
    }

    private void UpdateExpBar()
    {
        expBar.value = (float) currentExp / targetExp;
        levelText.text = ("LV " + currentLevel.ToString());
    }

}
