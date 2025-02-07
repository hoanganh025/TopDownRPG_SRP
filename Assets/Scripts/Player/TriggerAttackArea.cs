using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttackArea : MonoBehaviour
{
    private SwordAttack swordAttack;

    private void Awake()
    {
        swordAttack = GetComponentInParent<SwordAttack>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        swordAttack.DamagedEnemy(collision);
    }
}
