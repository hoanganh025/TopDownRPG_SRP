using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaExit : MonoBehaviour
{
    [SerializeField] private bool goNextLevel;
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            {
                SceneController.instanceSceneManager.NextScene();
            }
            else
            {
                SceneController.instanceSceneManager.LoadScene(sceneName);
            }
        }
    }
}
