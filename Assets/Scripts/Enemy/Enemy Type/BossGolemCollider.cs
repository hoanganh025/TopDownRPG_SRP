using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolemCollider : MonoBehaviour
{
    public BossGolem bossGolem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && bossGolem.isBossDashState)
        {
            bossGolem.DamageToPlayer(3, bossGolem.playerHeal);
        }
    }
}
