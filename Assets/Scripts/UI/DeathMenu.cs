using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ReStartGame()
    {
        //Reload current scene
        PlayerController.instance.gameObject.SetActive(true);
        GameEventManager.instance.levelEvent.LevelUp();
        PlayerController.instance.inputController.Enable();
        PlayerController.instance.transform.position = SceneController.instance.entrance.transform.position;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
