using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator animatorScene;
    public string sceneTransitionName { get; private set; }
    public GameObject firstPosPlayerInit;
    public static bool firstTimeLoadScene = false;

    //singleton
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //If the first time play game, player init center of scene 1
        if (firstTimeLoadScene == false)
        {
            PlayerController.instance.transform.position = firstPosPlayerInit.transform.position;
            firstTimeLoadScene = true;
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
    private IEnumerator LoadSceneRoutine() //Load next scene without parameter
    {
        animatorScene.SetTrigger("End");
        //Wait animation end scene finished
        yield return new WaitForSeconds(2f);
        //Load scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animatorScene.SetTrigger("Start");
    }

    private IEnumerator LoadSceneRoutine(string _sceneName) //Load next scene with scene name
    {
        animatorScene.SetTrigger("End");
        //Wait animation end scene finished
        yield return new WaitForSeconds(2f);
        //Load scene
        SceneManager.LoadSceneAsync(_sceneName);
        animatorScene.SetTrigger("Start");
    }
}
