using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    protected override void Awake()
    {
        base.Awake(); //Get new state machine and playerHealth from Awake() of Enemy class
    }

    protected override void Start()
    {
        base.Start();

        enemyStateMachine.Initialize(enemyPatrolState);
    }
}
