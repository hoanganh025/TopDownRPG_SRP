using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform playerTransform;
    private PlayerHeal playerHeal;
    private float timer = 2f;
    private float timeBetweenAttack = 2f;


    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.animator.SetTrigger("Attack");
        Debug.Log("Start attack state");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("End attack state");
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //enemy.EnemyMove(Vector2.zero);
        timer += Time.deltaTime;
        if (timer > timeBetweenAttack && enemy.isDeath == false)
        {
            timer = 0;
            enemy.animator.SetTrigger("Attack");
        }

        
        //If player out of attack distance, enemy back to chase state
        else if (!enemy.isAttackTrigger && enemy.isChaseTrigger && enemy.isDeath == false)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyChaseState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
