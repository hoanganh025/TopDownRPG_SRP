using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    [Header("Config")]
    private float health = 1;
    [SerializeField] private GameObject VFX_Bush;

    public void takeDamage(float damage)
    {
        Death(health);
    }

    public void Death(float currentHeath)
    {
        DestroyBush();
    }

    public void DestroyBush()
    {
        Instantiate(VFX_Bush, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
