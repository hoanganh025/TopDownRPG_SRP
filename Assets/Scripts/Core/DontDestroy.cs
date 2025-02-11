using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistantObjects = new GameObject[10];
    public int objectIndex;

    private void Awake()
    {
        if(persistantObjects[objectIndex] == null)
        {
            persistantObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
