using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolem_Laser : MonoBehaviour
{
    public float damageLaser = 5;
    private PlayerHeal playerHeal;

    private void Awake()
    {
        playerHeal = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerHeal>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHeal.takeDamage(damageLaser);
        }
    }
}
