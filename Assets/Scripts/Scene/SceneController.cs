using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneTransitionName { get; private set; }
    //singleton
    public static SceneController instanceSceneManager;

    private void Awake()
    {
        if (instanceSceneManager == null)
        {
            instanceSceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void NextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }

}
