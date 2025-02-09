using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    //Heal UI
    public Slider slider;
    public float manaRecoveryPerSecond = 0.5f;
    [SerializeField] private TMP_Text manabarText;
    public float currentMana { get; private set; }

    private float timer;
    private float timePerRecovery = 1;

    private void OnEnable()
    {
        GameEventManager.instance.levelEvent.onLevelUp += Fill;
        GameEventManager.instance.levelEvent.onLevelUp += UpdateManaBar;
    }

    private void OnDisable()
    {
        GameEventManager.instance.levelEvent.onLevelUp -= Fill;
        GameEventManager.instance.levelEvent.onLevelUp -= UpdateManaBar;
    }

    void Start()
    {
        currentMana = PlayerStat.instance.mana;
        UpdateManaBar();
    }

    void Update()
    {
        RecoveryManaPerSecond();
    }

    private void RecoveryManaPerSecond()
    {
        timer += Time.deltaTime;
        if (currentMana < PlayerStat.instance.mana)
        {
            if (timer > timePerRecovery)
            {
                RecoveryMana(manaRecoveryPerSecond);
                timer = 0;
            }
        }
    }

    public void ConsumptionMana(float manaValue)
    {
        currentMana = Mathf.Clamp(currentMana - manaValue, 0, PlayerStat.instance.mana);
        UpdateManaBar();
    }

    public bool CheckMana(float manaValue)
    {
        if (currentMana < manaValue)
        {
            Debug.Log("Out of mana");
            return false;
        }
        return true;
    }

    public void RecoveryMana(float manaValue)
    {
        currentMana = Mathf.Clamp(currentMana + manaValue, 0, PlayerStat.instance.mana);
        //Update mana bar
        UpdateManaBar();
    }

    public void UpdateManaBar()
    {
        slider.value = currentMana / PlayerStat.instance.mana;
        manabarText.text = currentMana.ToString() + "/" + PlayerStat.instance.mana.ToString();
    }

    public void Fill()
    {
        currentMana = PlayerStat.instance.mana; 
        Debug.Log("Player fill mana: " + currentMana);
        UpdateManaBar();
    }
}
