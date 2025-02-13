using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject boxDialog;
    public TMP_Text nameText;
    public TMP_Text dialogText;

    private Queue<string> sentences;

    public static DialogManager instance;

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

    void Start()
    {
        sentences = new Queue<string>();
        boxDialog.SetActive(false);
    }

    public void StartDialog(Dialog dialog)
    {
        //Prevent input to player when open UI
        Time.timeScale = 0;
        PlayerController.instance.isUIOpen = true;
        //Set dialog box to visible
        boxDialog.SetActive(true);
        //Set text of dialog box
        nameText.text = dialog.name;
        //Before start new dialog, delete pre dialog
        sentences.Clear();
        //For each sentence in list, get it
        foreach (string sentence in dialog.sentences)
        {
            //Add sentences to queue
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        //If player stop sentence and change to next one
        StopAllCoroutines();
        //Start next sentences
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        //Effect write each sentence
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

    private void EndDialog()
    {
        Time.timeScale = 1;
        PlayerController.instance.isUIOpen = false;
        boxDialog.SetActive(false);
    }
}
