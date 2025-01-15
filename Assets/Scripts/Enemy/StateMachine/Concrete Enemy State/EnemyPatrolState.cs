using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Vector3 targetPos;
    private Vector2 direction;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        targetPos = RandomPosInCircle();
        enemy.animator.SetBool("Patrol", true);

        Debug.Log("Start patrol state");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("End patrol state");
    }

    //This is Update()
    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!enemy.isAttackTrigger && enemy.isChaseTrigger && enemy.isDeath == false)
        {
            //Change to chase state
            stateMachine.ChangeState(enemy.enemyChaseState);
            enemy.animator.SetBool("Patrol", false);
        }

        //Get direction between target pos and enemy pos
        direction = (targetPos - enemy.transform.position).normalized;
        //Move to target pos
        enemy.EnemyMove(direction * enemy.randomMovementSpeed);
        //If enemy move to target pos, create new pos and move enemy to this
        if((enemy.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = RandomPosInCircle();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    private Vector3 RandomPosInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * enemy.randomMovementRange;
    }
}
