using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float damage;

    [Header("Patrol state variable")]
    public float randomMovementRange = 5f;
    public float randomMovementSpeed = 1f;

    public bool isChaseTrigger { get; set; }
    public bool isAttackTrigger { get; set; }

    public Rigidbody2D rigidBody2D;
    public bool isFacingRight = true;
    public bool isDeath = false;

    public PlayerHeal playerHeal;
    public GameObject player;

    public Animator animator;
    public EnemyHealthBar healthBar;
    protected Flash flash;

    public EnemyStateMachine enemyStateMachine;
    public EnemyPatrolState enemyPatrolState;
    public EnemyChaseState enemyChaseState;
    public EnemyAttackState enemyAttackState;

    protected virtual void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();

        //Initialize state at child class inheritant from this class
        enemyPatrolState = new EnemyPatrolState(this, enemyStateMachine);
        enemyChaseState = new EnemyChaseState(this, enemyStateMachine);
        enemyAttackState = new EnemyAttackState(this, enemyStateMachine);

        playerHeal = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerHeal>();
        player = GameObject.FindWithTag("Player");
    }

    protected virtual void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar = GetComponent<EnemyHealthBar>();

        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        enemyStateMachine.currentEnemyState.FrameUpdate();
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

        if(healthBar != null)
        {
            healthBar.UpdateEnemyHealthBar(maxHealth, currentHealth);
        }

        //When enemy death
        Death(currentHealth);
    }

    public virtual void Death(float currentHeath)
    {
        if (currentHealth <= 0 && !isDeath)
        {
            //Animation death
            animator.SetTrigger("Death");
            isDeath = true;

            //Player receives exp
            GameEventManager.instance.levelEvent.ExpGained(exp);

            //turn off behaviour 
            rigidBody2D.isKinematic = true;
            slider.gameObject.SetActive(false);
            //flash.gameObject.SetActive(false);
            slider.enabled = false;
            this.enabled = false;
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

    //Deal damage to player, attach to animation attack 
    public void DamageToPlayer(float _damage, PlayerHeal playerHeal)
    {
        playerHeal.takeDamage(_damage);
    }

    //Attach in end frame slime attack
    public void DamageToPlayerNoParameter()
    {
        playerHeal.takeDamage(damage);
    }

    public void Healing(float healAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
        Debug.Log(currentHealth);
        healthBar.UpdateEnemyHealthBar(maxHealth, currentHealth);
    }
}
