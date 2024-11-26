using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; }

    private Animator animator;
    private KnockBack knockBackScripts;
    private Flash flashScrips;

    //delegete
    public delegate void OnHit();
    //delegete instance
    public static event OnHit onHit;

    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        knockBackScripts = GetComponent<KnockBack>();
        flashScrips = GetComponent<Flash>();
    }

    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        //set knockback
        if(knockBackScripts != null)
        {
            knockBackScripts.GetKnockBack(PlayerController.getInstancePlayer().transform);
        }

        //set flash when be hit 
        if (flashScrips != null)
        {
            StartCoroutine(flashScrips.FlashRoutine());
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
