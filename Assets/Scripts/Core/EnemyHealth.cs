using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; }

    private Animator animator;

    //delegete
    public delegate void OnHit();
    //delegete instance
    public static event OnHit onHit;

    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("Death");
            
            //turn off behaviour 

        }
    }

    //Destroy gameobject, it's set in end of EnemyDeath frame 
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
