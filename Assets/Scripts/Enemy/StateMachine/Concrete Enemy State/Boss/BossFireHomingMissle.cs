using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireHomingMissle : EnemyState
{
    public BossFireHomingMissle(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //Create homing missle in the end frame of this animation
        bossGolem.animator.SetBool("HomingMissle", true);
    }

    public override void ExitState()
    {
        base.ExitState();

        bossGolem.animator.SetBool("HomingMissle", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        bossGolem.rigidBody2D.velocity = Vector2.zero;
    }
}
