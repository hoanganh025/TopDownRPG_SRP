using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BossDashState : EnemyState
{
    private Vector3 playerPos;
    private float dashSpeed = 8;
    private float timer;
    private float timeBeforeDash = 0.3f;

    public BossDashState(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        playerPos = bossGolem.player.transform.position;
        bossGolem.animator.SetBool("Dash", true);
        timer = 0;
        //Only damage player by body boss when in the dash state
        bossGolem.isBossDashState = true;
    }

    public override void ExitState()
    {
        base.ExitState();

        bossGolem.animator.SetBool("Dash", false);
        bossGolem.isBossDashState = false;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        playerPos = bossGolem.player.transform.position;
        timer += Time.deltaTime;

        if(timer > timeBeforeDash)
        {
            bossGolem.animator.SetBool("Dash", true);
            //Get the direction
            Vector2 direction = (playerPos - bossGolem.transform.position).normalized;
            bossGolem.EnemyMove(direction * dashSpeed);
            ChangeState(bossGolem.bossChaseState);
            timer = 0;
        }
    }
 

    //Wait until laser beam animation completely
    private async void ChangeState(EnemyState enemyState)
    {
        await Task.Delay(2000);
        bossGolem.enemyStateMachine.ChangeState(enemyState);
    }
}
