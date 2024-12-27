using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator animatorScene;
    public string sceneTransitionName { get; private set; }
    //singleton
    public static SceneController instanceSceneManager;

    private void Awake()
    {
        if (instanceSceneManager == null)
        {
            instanceSceneManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void NextScene()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    public void LoadSceneByName(string _sceneName)
    {
        StartCoroutine(LoadSceneRoutine(_sceneName));
    }

    //Overloading LoadSceneRoutine 
    private IEnumerator LoadSceneRoutine()
    {
        animatorScene.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animatorScene.SetTrigger("Start");
    }
    private IEnumerator LoadSceneRoutine(string _sceneName)
    {
        animatorScene.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(_sceneName);
        animatorScene.SetTrigger("Start");
    }
}
