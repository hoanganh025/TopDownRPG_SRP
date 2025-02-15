using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public bool isTutorialActive = false;

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
            isTutorialActive = true;
        }
        else
        {
            tutorialCanvas.SetActive(false);
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
