using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Entrance entrance;

    private void Awake()
    {
        gameObject.SetActive(false);
        entrance = GameObject.Find("AreaEntrance").GetComponent<Entrance>();
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ReStartGame()
    {
        //resetData();
        //Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerController.instance.transform.position = entrance.transform.position;
        gameObject.SetActive(false );
    }

    private void resetData()
    {
        /*foreach (GameObject gOb in listGameObjectDontDestroy)
        {
            Destroy(gOb);
        }*/
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
