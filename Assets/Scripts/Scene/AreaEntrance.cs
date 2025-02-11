using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private void Start()
    {
        //If not the first time player init, player will appear in entrance area
        if(SceneController.firstTimeLoadScene == true)
        {
            PlayerController.instance.transform.position = this.transform.position;
        }
    }
}
