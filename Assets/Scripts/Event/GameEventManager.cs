using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance {  get; private set; }

    public QuestEvent questEvent;
    public PlayerDeathEvent playerDeathEvent;
    public InputEvent inputEvent;
    public LevelEvent levelEvent;
    public GoldEvent goldEvent;
    public CollectedQuestItemEvent collectedQuestItemEvent;
    public SceneTransitionEvent sceneTransitionEvent;
    public EquipWeaponEvent equipWeaponEvent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Initialize
        questEvent = new QuestEvent();
        playerDeathEvent = new PlayerDeathEvent();
        inputEvent = new InputEvent();
        levelEvent = new LevelEvent();
        goldEvent = new GoldEvent();
        collectedQuestItemEvent = new CollectedQuestItemEvent();
        sceneTransitionEvent = new SceneTransitionEvent();
        equipWeaponEvent = new EquipWeaponEvent();
    }
}
