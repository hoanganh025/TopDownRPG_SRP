using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    public float timeLife = 2;
    private Animator animator;
    private float timer;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        AudioManager.instance.playSFX(AudioManager.instance.magicShoot);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeLife)
        {
            AudioManager.instance.playSFX(AudioManager.instance.magicExplore);
            animator.SetTrigger("Explosion");
            timer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            AudioManager.instance.playSFX(AudioManager.instance.magicExplore);
            rb.velocity = Vector2.zero;
            damageable.takeDamage(PlayerStat.instance.AP);
            animator.SetTrigger("Explosion");
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
