using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float attackDuration = 0.2f;
    public BoxCollider2D attackCollider;

    private Vector2 attackDirection;

    void Start()
    {
        attackCollider.enabled = false;
    }

    public void AttackWithDirection(Vector2 direction, Vector2 offset)
    {
        //Change size collider when up/down or left/right attack
        if(direction == Vector2.up || direction == Vector2.down)
        {
            attackCollider.size = new Vector2(1, 1.6f);
        }
        else if(direction == Vector2.right || direction == Vector2.left)
        {
            attackCollider.size = new Vector2(2, 0.7f);
        }

        //Set offset 
        attackCollider.offset = offset;
        //Turn on collider
        attackCollider.enabled = true;
        //Turn off collider after 0.2s
        Invoke(nameof(DisableCollider), attackDuration);
    }

    //Set this in end of frame attack up animation
    public void AttackUp()
    {
        AudioManager.instance.playSFX(AudioManager.instance.slashSword);
        AttackWithDirection(Vector2.up, new Vector2(0, 1));
    }

    public void AttackDown()
    {
        AudioManager.instance.playSFX(AudioManager.instance.slashSword);
        AttackWithDirection(Vector2.down, new Vector2(0, -0.15f));
    }

    public void AttackLeft()
    {
        AudioManager.instance.playSFX(AudioManager.instance.slashSword);
        AttackWithDirection(Vector2.left, new Vector2(-0.7f, 0.5f));
    }

    public void AttackRight()
    {
        AudioManager.instance.playSFX(AudioManager.instance.slashSword);
        AttackWithDirection(Vector2.right, new Vector2(0.7f, 0.5f));
    }

    private void DisableCollider()
    {
        attackCollider.enabled = false;
    }

    //When trigger in TriggerAttackArea script is triggred, call this
    public void DamagedEnemy(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.takeDamage(PlayerStat.instance.attack);
        }
    }
}
