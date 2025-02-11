using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Config")]
    [SerializeField] private int exp;
    [SerializeField] private float maxHealth;
    public float currentHealth {  get; private set; }

    private Animator animator;

    //Enemy Health bar
    private EnemyHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar = GetComponent<EnemyHealthBar>();
    }

    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

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

        healthBar.UpdateEnemyHealthBar(maxHealth, currentHealth);

        //When enemy death
        Death(currentHealth);
    }

    public void Death(float currentHeath)
    {
        if (currentHealth <= 0)
        {
            //animation death
            animator.SetTrigger("Death");

            //Player receives exp
            GameEventManager.instance.levelEvent.ExpGained(exp);

            //turn off behaviour 

        }
    }

    //Destroy gameobject, it's set in end of EnemyDeath frame 
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
