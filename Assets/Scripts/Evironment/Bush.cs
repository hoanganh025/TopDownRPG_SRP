using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject VFX_Bush;

    public void takeDamage(float damage)
    {
        DestroyBush();
    }

    public void DestroyBush()
    {
        Instantiate(VFX_Bush, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
