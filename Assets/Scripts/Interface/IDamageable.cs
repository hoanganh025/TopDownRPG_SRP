using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void takeDamage(float damage);
    void Death(float currentHeath);
}
