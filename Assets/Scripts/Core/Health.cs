using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; } 

    private Animator animator;

    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth <= 0)
        {
            //animation death
            animator.SetTrigger("Death");
            
            //turn off behaviour 

        }
    }

    //Destroy gameobject
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
