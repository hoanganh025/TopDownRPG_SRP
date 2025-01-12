using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance {  get; private set; }

    public QuestEvent questEvent;
    public MiscEvent miscEvent;
    public InputEvent inputEvent;
    public LevelEvent levelEvent;
    public GoldEvent goldEvent;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        //Initialize
        questEvent = new QuestEvent();
        miscEvent = new MiscEvent();
        inputEvent = new InputEvent();
        levelEvent = new LevelEvent();
        goldEvent = new GoldEvent();
    }
}
