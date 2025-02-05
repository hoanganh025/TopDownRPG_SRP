using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : EnemyState
{
    private Vector3 playerPos;
    private float bossSpeed = 2;
    private float timer = 0;
    private float timeBeforeUseSkill = 3f;

    public BossChaseState(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        bossGolem.animator.SetBool("Chase", true);
        timer = 0;
        Debug.Log("Chase state");
    }

    public override void ExitState()
    {
        base.ExitState();

        bossGolem.animator.SetBool("Chase", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer += Time.deltaTime;

        //Chase player
        playerPos = bossGolem.player.transform.position;
        Vector2 direction = (playerPos - bossGolem.transform.position).normalized;
        bossGolem.EnemyMove(direction * bossSpeed);
        
        //Time between skill
        if (timer > timeBeforeUseSkill)
        {
            //When entering attack area
            if (bossGolem.isAttackTrigger && !bossGolem.isDeath)
            {
                bossGolem.enemyStateMachine.ChangeState(bossGolem.bossAttackState);
            }

            //When entering dash area
            else if(bossGolem.isDashTrigger && !bossGolem.isDeath)
            {
                bossGolem.enemyStateMachine.ChangeState(bossGolem.bossDashState);
            }

            //Random 2 skill if entering homingmissle attack
            else if (bossGolem.isHomingMissleTrigger && !bossGolem.isDashTrigger && !bossGolem.isDeath)
            {
                //bossGolem.enemyStateMachine.ChangeState(bossGolem.bossFireHomingMissleState);
                float randomNum = Random.Range(1, 3);
                Debug.Log(randomNum);
                switch (randomNum)
                {
                    case 1:
                        bossGolem.enemyStateMachine.ChangeState(bossGolem.bossLaserBeamState);
                        break;
                    case 2:
                        bossGolem.enemyStateMachine.ChangeState(bossGolem.bossFireHomingMissleState);
                        break;
                }
            }
        }

    }

}
