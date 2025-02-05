using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossHealingState : EnemyState
{
    //Heal amount per second
    private float healAmount = 1f;
    private float healTimer = 0;
    public BossHealingState(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        healTimer = 0;
        bossGolem.animator.Play("Healing");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        healTimer += Time.deltaTime;

        //Heal per second
        if(healTimer > 1f)
        {
            bossGolem.Healing(healAmount);
            healTimer = 0;
        }

        ChangeState(3000);
    }

    //Wait until laser beam animation completely
    private async void ChangeState(int timeDelay)
    {
        await Task.Delay(timeDelay);

        if (bossGolem.currentHealth >= bossGolem.maxHealth * 2 / 3)
        {
            bossGolem.enemyStateMachine.ChangeState(bossGolem.bossChaseState);
        }
        else
        {
            bossGolem.enemyStateMachine.ChangeState(bossGolem.bossDashState);
        }
    }

}
