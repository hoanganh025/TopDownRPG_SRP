using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour, IDamageable
{
    //Heal UI
    public Slider slider;

    public float currentHealth { get; private set; }

    private void OnEnable()
    {
        GameEventManager.instance.levelEvent.onLevelUp += Fill;
        GameEventManager.instance.levelEvent.onLevelUp += UpdateHealthBar;
    }

    private void OnDisable()
    {
        GameEventManager.instance.levelEvent.onLevelUp -= Fill;
        GameEventManager.instance.levelEvent.onLevelUp -= UpdateHealthBar;
    }

    private void Start()
    {
        currentHealth = PlayerStat.instance.health;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeDamage(1);
            UpdateHealthBar();
        }
    }

    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, PlayerStat.instance.health);

        //set knockback
        if (gameObject.TryGetComponent<KnockBack>(out var knockback))
        {
            knockback.GetKnockBack(PlayerController.instance.transform);
        }

        //set flash when be hit 
        if (gameObject.TryGetComponent<Flash>(out var flash))
        {
            StartCoroutine(flash.FlashRoutine());
        }

        //Update health bar
        UpdateHealthBar();

        //When player death
        Death(currentHealth);
    }

    public void Death(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            //animation death
            Debug.Log("Death");
        }
    }

    public void Healing(float _healValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healValue, 0, PlayerStat.instance.health);
        //Update health bar
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        slider.value = currentHealth / PlayerStat.instance.health;
    }

    public void Fill()
    {
        currentHealth = PlayerStat.instance.health;
        UpdateHealthBar();
    }
}
