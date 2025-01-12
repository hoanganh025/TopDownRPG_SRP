using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] private int startingGold;
    [SerializeField] private TMP_Text goldText;
    public int currentGold { get; private set; }

    private void Awake()
    {
        currentGold = startingGold;
    }

    private void OnEnable()
    {
        GameEventManager.instance.goldEvent.onGoldGained += GoldGained;
    }

    private void OnDisable()
    {
        GameEventManager.instance.goldEvent.onGoldGained -= GoldGained;
    }

    private void GoldGained(int _amount)
    {
        currentGold += _amount;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        goldText.text = currentGold.ToString();
    }
}
