using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class BossLaserBeamState : EnemyState
{
    private Vector3 playerPos;
    private float timer;
    private float timeBeforeLaser = 0.5f;
    private bool isFire;


    public BossLaserBeamState(BossGolem bossGolem, EnemyStateMachine stateMachine) : base(bossGolem, stateMachine)
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
        bossGolem.animator.SetBool("LaserBeam", true);
        isFire = false;
        Debug.Log("Laser state");
    }

    public override void ExitState()
    {
        base.ExitState();
        bossGolem.animator.SetBool("LaserBeam", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        timer += Time.deltaTime;

        //Lock position player 0.5s before fire laser and only fire one time
        if (timer > timeBeforeLaser && !isFire)
        {
            bossGolem.rigidBody2D.velocity = Vector2.zero;
            Vector2 direction = (playerPos - bossGolem.transform.position).normalized;
            //Get euler between boss position and player position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Convert euler to quaternion
            var rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            //Set quaternion to prefab
            bossGolem.CreateLaserBeam(rotation);

            timer = 0;
            isFire = true;
            ChangeState(bossGolem.bossChaseState);
        }
    }

    //Wait until laser beam animation completely
    private async void ChangeState(EnemyState enemyState)
    {
        await Task.Delay(2000);
        bossGolem.enemyStateMachine.ChangeState(enemyState);
    }

}

