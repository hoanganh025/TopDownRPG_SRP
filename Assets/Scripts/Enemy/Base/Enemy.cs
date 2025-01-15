using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    //Heal UI
    public Slider slider;

    [Header("Config")]
    public float maxHealth;
    public float currentHealth { get; private set; }
    public int exp;

    [Header("Patrol state variable")]
    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;

    public bool isChaseTrigger { get; set; }
    public bool isAttackTrigger { get; set; }

    public Rigidbody2D rigidBody2D;
    public bool isFacingRight = true;
    public bool isDeath = false;

    public Animator animator;
    private EnemyHealthBar healthBar;

    public EnemyStateMachine enemyStateMachine;
    public EnemyPatrolState enemyPatrolState;
    public EnemyChaseState enemyChaseState;
    public EnemyAttackState enemyAttackState;

    protected virtual void Awake()
    {
        enemyStateMachine = new EnemyStateMachine(); 

        enemyPatrolState = new EnemyPatrolState(this, enemyStateMachine);
        enemyChaseState = new EnemyChaseState(this, enemyStateMachine);
        enemyAttackState = new EnemyAttackState(this, enemyStateMachine);
    }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar = GetComponent<EnemyHealthBar>();

        currentHealth = maxHealth;

        enemyStateMachine.Initialize(enemyPatrolState);
    }

    private void Update()
    {
        enemyStateMachine.currentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentEnemyState.PhysicUpdate();
    }

    #region Heal/Die Function
    public void takeDamage(float damage)
    {
        //set health always between 0 and starting health 0<currentHealth<startingHealth
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        //set knockback
        if (gameObject.TryGetComponent<KnockBack>(out var knockback))
        {
            knockback.GetKnockBack(PlayerController.instance.transform);
        }

        //set flash when be hit 
        if (gameObject.TryGetComponent<Flash>(out var flash))
        {
            StartCoroutine(flash.FlashRoutine());
        }

        healthBar.UpdateEnemyHealthBar(maxHealth, currentHealth);

        //When enemy death
        Death(currentHealth);
    }

    public void Death(float currentHeath)
    {
        if (currentHealth <= 0)
        {
            //Animation death
            animator.SetTrigger("Death");
            isDeath = true;

            //Player receives exp
            GameEventManager.instance.levelEvent.ExpGained(exp);

            //turn off behaviour 

        }
    }

    //Destroy gameobject, it's set in end of EnemyDeath frame 
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Move Function

    public void EnemyMove(Vector2 velocity)
    {
        rigidBody2D.velocity = velocity;
        CheckForLeftOrRightFacing(velocity);
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        //If facing right but velocity < 0
        if(isFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
        }

        //If facing right but velocity > 0
        else if (!isFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;
        }
    }

    #endregion

    #region Animation Trigger

    private void AnimationTrigger(AnimationTriggerType triggerType)
    {
        enemyStateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    #endregion

    #region Check Distance
    public void SetChaseStatus(bool chaseStatus)
    {
        isChaseTrigger = chaseStatus;
    }

    public void SetAttackStatus(bool attackStatus)
    {
        isAttackTrigger = attackStatus;
    }
    #endregion
}
