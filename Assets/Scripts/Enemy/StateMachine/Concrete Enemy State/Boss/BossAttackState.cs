using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossAttackState : EnemyState
{
    private float timer = 0;
    private float timeBetweenAttack = 1.5f;

    public BossAttackState(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //When enter state, can attack in firt time
        timer = 2;
        bossGolem.animator.SetTrigger("Attack");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer += Time.deltaTime;
        
        //While still in the attack area
        if (timer > timeBetweenAttack && !bossGolem.isDeath)
        {
            bossGolem.rigidBody2D.velocity = Vector2.zero;
            bossGolem.animator.SetTrigger("Attack");
            timer = 0;
        }

        //When exit attack area
        else if (!bossGolem.isAttackTrigger && !bossGolem.isDeath)
        {
            bossGolem.enemyStateMachine.ChangeState(bossGolem.bossChaseState);
        }
    }

}
