using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialCanvas;
    private bool isTutorialActive = false;

    private void Start()
    {
        tutorialCanvas.SetActive(false);
    }

    public void PressButtonPlay()
    {
        SceneController.instance.LoadSceneByName("Scene1");
    }

    //When press button tutorial
    public void PressButtonTutorial()
    {
        if (!isTutorialActive)
        {
            tutorialCanvas.SetActive(true);
            Time.timeScale = 0;
            isTutorialActive = true;
        }
        else
        {
            tutorialCanvas.SetActive(false);
            Time.timeScale = 1;
            isTutorialActive = false;
        }
    }

    public void PressButtonExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif  
    }

    //When click button X
    public void CloseTutorial()
    {
        tutorialCanvas.SetActive(false);
        isTutorialActive = false;
    }
}
