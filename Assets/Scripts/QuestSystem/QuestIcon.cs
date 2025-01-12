using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icon")]
    [SerializeField] private GameObject requimentNotMetToStartIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject requimentNotMetToFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetStateIcon(QuestState newState, bool startPoint, bool finishPoint)
    {
        requimentNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requimentNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch(newState)
        {
            case QuestState.REQUIMENT_NOT_MET:
                if(startPoint)
                {
                    requimentNotMetToStartIcon.SetActive(true);
                }
                break;

            case QuestState.CAN_START:
                if (startPoint)
                {
                    canStartIcon.SetActive(true);
                }
                break;

            case QuestState.IN_PROGRESS:
                if (finishPoint)
                {
                    requimentNotMetToFinishIcon.SetActive(true);
                }
                break;

            case QuestState.CAN_FINISH:
                if (finishPoint)
                {
                    canFinishIcon.SetActive(true);
                }
                break;

            case QuestState.FINISHED:
                break;

            default:
                Debug.Log("Not recognized quest state: " + newState);
                break;
        }
    }
}
