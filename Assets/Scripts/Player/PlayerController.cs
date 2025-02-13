using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private bool canDash = true;

    [Header("Experience")]
    public float currentExp;
    public float maxExp;
    
    public bool facingLeft = true;
    public bool isUIOpen = false;

    public InputController inputController;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 dirMove;
    private TrailRenderer trailRenderer;
    private PlayerMana playerMana;

    // singleton
    public static PlayerController instance;

    private void Awake()
    {
        //check to avoid duplicate instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        inputController = new InputController();
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        animator = rb.GetComponent<Animator>();
        trailRenderer = rb.GetComponent<TrailRenderer>();
        playerMana = GetComponentInChildren<PlayerMana>();
    }

    private void OnEnable()
    {
        if(inputController != null)
        {
            inputController.Enable();
        }
    }

    private void OnDisable()
    {
        if(inputController != null)
        {
            inputController.Disable();
        }
    }

    void Start()
    {
        //turn off trailrenderer
        trailRenderer.emitting = false;
    }

    void Update()
    {
        //get direction and vecto
        //dirMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (isUIOpen) return;

        GetDirectionPlayerMove();

        AttackWithMouseDirection();
    }
    
    private void FixedUpdate()
    {
        if (isUIOpen) return;
        Move();

        //Dash when space down
        if (inputController.Gameplay.Dash.WasPressedThisFrame())
        {
            Dash();
        }
    }

    private void GetDirectionPlayerMove()
    {
        dirMove = inputController.Gameplay.Movement.ReadValue<Vector2>();
        animator.SetFloat("Horizontal", dirMove.x);
        animator.SetFloat("Vertical", dirMove.y);
        animator.SetFloat("Speed", dirMove.sqrMagnitude);
    }

    private void Move()
    {
        //if magnitude > 1 then round the dirMove because normalized round even if vecto < 1
        if (dirMove.magnitude > 1f)
        {
            dirMove = dirMove.normalized;
        }

        //caculate velocity
        rb.velocity = dirMove * PlayerStat.instance.agility;
        //set animation parameter
        //animator.SetBool("Run", dirMove.x != 0 || dirMove.y != 0);
    }

    private void AttackWithMouseDirection()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //If pause game, stop input
            if (Time.timeScale == 0)
            {
                return;
            }

            animator.SetTrigger("Attack");
        }

        //Get the mouse position 
        Vector3 mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouDirection = (mouPos - PlayerController.instance.transform.position).normalized;
        Vector2 mouDirection_Sign = Vector2.zero;

        //Set sign index to prevent blend tree
        if (Mathf.Abs(mouDirection.x) > Mathf.Abs(mouDirection.y))
        {
            mouDirection_Sign = new Vector2(Mathf.Sign(mouDirection.x), 0);
        }
        else
        {
            mouDirection_Sign = new Vector2(0, Mathf.Sign(mouDirection.y));
        }

        animator.SetFloat("MouseX", mouDirection_Sign.x);
        animator.SetFloat("MouseY", mouDirection_Sign.y);
    }

    //dash
    private void Dash()
    {
        if (canDash && playerMana.CheckMana(5))
        {
            AudioManager.instance.playSFX(AudioManager.instance.playerDash);
            playerMana.ConsumptionMana(5);
            PlayerStat.instance.agility += dashForce;
            
            //turn on trailRenderer
            trailRenderer.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }

    public IEnumerator DashRoutine()
    {
        canDash = false;
        //wait time to dash
        yield return new WaitForSeconds(dashTime);
        PlayerStat.instance.agility -= dashForce;
        //turn off trailRenderer
        trailRenderer.emitting = false;
        //wait time dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void footStep()
    {
        AudioManager.instance.playSFX(AudioManager.instance.playerFootStep);
    }
}
