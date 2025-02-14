using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour, IDamageable
{
    //Heal UI
    public Slider slider;

    public float currentHealth { get; private set; }
    [SerializeField] private TMP_Text healthbarText;

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

    }

    public void takeDamage(float damage)
    {
        AudioManager.instance.playSFX(AudioManager.instance.playerHurt);
        float damageAfterArmor = CaculatedDamage(damage);
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damageAfterArmor, 0, PlayerStat.instance.health);

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

    //Damage take after redution by defense stat
    private float CaculatedDamage(float damage)
    {
        float reduction = Mathf.Clamp01(PlayerStat.instance.defense / 100f);
        if (reduction > 0.6f)
        {
            reduction = 0.6f;
        }
        return damage -= damage*reduction;
    }

    public void Death(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            //animation death
            GameEventManager.instance.playerDeathEvent.PlayerDeath();
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
        healthbarText.text = currentHealth.ToString("F1") + "/" + PlayerStat.instance.health.ToString();
    }

    public void Fill()
    {
        currentHealth = PlayerStat.instance.health;
        UpdateHealthBar();
    }
}
