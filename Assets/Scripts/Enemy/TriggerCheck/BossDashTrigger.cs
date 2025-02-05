using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashTrigger : MonoBehaviour
{
    private BossGolem bossGolem;

    private void Awake()
    {
        bossGolem = GetComponentInParent<BossGolem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossGolem.isDashTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossGolem.isDashTrigger = false;
        }
    }
}
