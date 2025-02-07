using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGolem : Enemy
{
    public BossChaseState bossChaseState;
    public BossDashState bossDashState;
    public BossAttackState bossAttackState;
    public BossHealingState bossHealingState;
    public BossLaserBeamState bossLaserBeamState;
    public BossFireHomingMissle bossFireHomingMissleState;

    public bool isHomingMissleTrigger;
    public bool isDashTrigger;
    public bool isLaserBeamTrigger;

    [Header("Prefab skill object")]
    public GameObject BossArm;

    public GameObject homingMissleFirePos;
    public GameObject laserBeamFirePos;
    public GameObject laserBeamPrefab;

    private bool healStateIsActive = false;
    public bool isFacingLeft;
    public bool isBossDashState = false;

    protected override void Awake()
    {
        base.Awake(); //Get new state machine and playerHealth from Awake() of Enemy class

        bossChaseState = new BossChaseState(this, enemyStateMachine);
        bossDashState = new BossDashState(this, enemyStateMachine);
        bossAttackState = new BossAttackState(this, enemyStateMachine);
        bossHealingState = new BossHealingState(this, enemyStateMachine);
        bossLaserBeamState = new BossLaserBeamState(this, enemyStateMachine);
        bossFireHomingMissleState = new BossFireHomingMissle(this, enemyStateMachine);

        flash = GameObject.Find("Boss").GetComponent<Flash>();
    }
    protected override void Start()
    {
        base.Start();

        enemyStateMachine.Initialize(bossChaseState);
        //enemyStateMachine.Initialize(bossFireHomingMissleState);
    }

    protected override void Update()
    {
        base.Update();
        CheckBossPosition();

        //Only healing one time
        if (currentHealth <= maxHealth / 2 && !healStateIsActive)
        {
            healStateIsActive = true;
            enemyStateMachine.ChangeState(bossHealingState);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyStateMachine.currentEnemyState.TriggerEnter(collision);
    }

    private void CheckBossPosition()
    {
        //Check position of boss compared to player
        if (transform.position.x < player.transform.position.x)
        {
            isFacingLeft = false;
        }
        else
        {
            isFacingLeft = true;
        }
    }


    //Create after fire homing missle, attach in frame HomingMissle
    public void CreateHomingMissle()
    {
        //Create homing missle with direction
        if(!isFacingLeft)
        {
            Instantiate(BossArm, homingMissleFirePos.transform.position, Quaternion.identity);
        }
        else
        {
            Quaternion quaternionArmBoss = Quaternion.Euler(new Vector3(0, 180, 0));
            Instantiate(BossArm, homingMissleFirePos.transform.position, quaternionArmBoss);
        }
        //After fire, dash to player
        enemyStateMachine.ChangeState(bossDashState);
    }

    //Create laser prefab when play laser beam animation
    public void CreateLaserBeam(Quaternion rotation)
    {
        Instantiate(laserBeamPrefab, laserBeamFirePos.transform.position, rotation);
    }

    //Attach to end frame of animation attack
    public void DamageMelleToPlayer()
    {
        playerHeal.takeDamage(5);
    }

}
