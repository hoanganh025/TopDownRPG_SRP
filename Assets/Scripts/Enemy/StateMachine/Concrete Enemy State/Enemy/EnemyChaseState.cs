using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private Transform playerTransform;
    private float enemyMoveSpeed = 1.75f;
    private Vector2 direction;

    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //Set animation from patrol to chase
        enemy.animator.SetBool("Chase", true);
    }

    public override void ExitState()
    {
        base.ExitState();

        enemy.animator.SetBool("Chase", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        direction = (playerTransform.position - enemy.transform.position).normalized;
        enemy.EnemyMove(direction * enemyMoveSpeed);

        if (enemy.isAttackTrigger && enemy.isDeath == false)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyAttackState);
        }

        //If player out of chase and attack distance, enemy back to patrol state
        else if (!enemy.isAttackTrigger && !enemy.isChaseTrigger && enemy.isDeath == false)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyPatrolState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
