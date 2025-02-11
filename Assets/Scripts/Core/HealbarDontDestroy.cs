using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealbarDontDestroy : MonoBehaviour
{
    public static HealbarDontDestroy instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
