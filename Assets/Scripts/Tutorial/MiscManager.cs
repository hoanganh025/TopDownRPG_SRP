using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscManager : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject winCanvas;
    private bool isTutorialActive = false;

    public static MiscManager instance;
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
        tutorialCanvas.SetActive(false);
        winCanvas.SetActive(false);
    }

    private void Update()
    {
        if (PlayerController.instance.inputController.Gameplay.Instruction.WasPressedThisFrame())
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
    }

    //Active when player win
    public void Win()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
