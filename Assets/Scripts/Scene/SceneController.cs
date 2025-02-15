using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator animatorScene;
    public Entrance entrance;
    public string sceneTransitionName { get; private set; }
    public GameObject firstPosPlayerInit;
    public static bool firstTimeLoadScene = false;
    public QuestPoint questPoint;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        //If the first time play game, player init center of scene 1
        if (PlayerController.instance && firstTimeLoadScene == false && firstPosPlayerInit)
        {
            Debug.Log(firstPosPlayerInit);
            PlayerController.instance.transform.position = firstPosPlayerInit.transform.position;
            firstTimeLoadScene = true;
        }
    }

    public void NextScene()
    {
        StartCoroutine(LoadSceneRoutine());
        if (questPoint)
        {
            GameEventManager.instance.sceneTransitionEvent.QuestSceneTransition(questPoint.questID, questPoint.currentQuestState);
        }
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
        Debug.Log("Load scene 1");
        animatorScene.SetTrigger("End");
        //Wait animation end scene finished
        yield return new WaitForSeconds(2f);
        //Load scene
        SceneManager.LoadSceneAsync(_sceneName);
        animatorScene.SetTrigger("Start");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "Scene0")
        {
            entrance = GameObject.Find("AreaEntrance").GetComponent<Entrance>();
        }
    }
}
