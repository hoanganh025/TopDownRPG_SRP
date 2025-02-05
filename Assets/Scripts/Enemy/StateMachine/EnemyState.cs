using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected Slime slime;
    protected BossGolem bossGolem;
    protected EnemyStateMachine stateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }
    public EnemyState(BossGolem bossGolem, EnemyStateMachine stateMachine)
    {
        this.bossGolem = bossGolem;
        this.stateMachine = stateMachine;
    }

    //Virtual method can be overriding
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicUpdate() { }
    public virtual void TriggerEnter(Collider2D collision) { }
    public virtual void TriggerStay(Collider2D collision) { }
    public virtual void TriggerExit(Collider2D collision) { }
    public virtual void CollisionEnter(Collision2D collision) { }
    public virtual void CollisionStay(Collision2D collision) { }
    public virtual void CollisionExit(Collision2D collision) { }
    public virtual void AnimationTriggerEvent(AnimationTriggerType triggerType) { }
}
