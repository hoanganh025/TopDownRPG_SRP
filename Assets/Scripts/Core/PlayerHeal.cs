using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour, IDamageable
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }

    private void Start()
    {
        currentHealth = startingHealth;
        Debug.Log(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeDamage(1);
            Debug.Log(currentHealth);
        }
    }

    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        //set knockback
        if (gameObject.TryGetComponent<KnockBack>(out var knockback))
        {
            knockback.GetKnockBack(PlayerController.InstancePlayer.transform);
        }

        //set flash when be hit 
        if (gameObject.TryGetComponent<Flash>(out var flash))
        {
            StartCoroutine(flash.FlashRoutine());
        }

        if (currentHealth <= 0)
        {
            //animation death
            Debug.Log("Dath");
        }
    }

    public void Healing(float _healValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healValue, 0, startingHealth);
        Debug.Log(currentHealth);
    }


}
