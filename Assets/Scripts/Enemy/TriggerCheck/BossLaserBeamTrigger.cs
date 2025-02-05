using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserBeamTrigger : MonoBehaviour
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
            bossGolem.isLaserBeamTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossGolem.isLaserBeamTrigger = false;
        }
    }
}
