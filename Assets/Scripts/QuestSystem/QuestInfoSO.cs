using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObject/QuestInfoSO")]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id {  get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requiment")]
    public int levelRequiment;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int expReward;

    //When have change in editor, OnValidate is called automatically
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
