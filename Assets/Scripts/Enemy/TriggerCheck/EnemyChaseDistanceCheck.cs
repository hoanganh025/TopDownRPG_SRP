using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseDistanceCheck : MonoBehaviour
{
    private GameObject player;
    private Enemy enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            enemy.isChaseTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            enemy.isChaseTrigger = false;
        }
    }
}
