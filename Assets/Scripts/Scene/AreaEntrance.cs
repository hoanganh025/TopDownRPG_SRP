using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private void Start()
    {
        /*if (transitionName == SceneController.getinstanceSceneManagement().sceneTransitionName)
        {
            PlayerController.instance.transform.position = this.transform.position;
        }*/
        PlayerController.instance.transform.position = this.transform.position;
    }
}
