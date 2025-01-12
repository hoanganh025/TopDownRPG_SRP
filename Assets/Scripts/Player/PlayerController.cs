using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private bool canDash = true;

    [Header("Experience")]
    public float currentExp;
    public float maxExp;
    
    public bool facingLeft = true;

    private InputController inputController;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 dirMove;
    private TrailRenderer trailRenderer;

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
        dirMove = inputController.Gameplay.Movement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
        FacingWithMouse();

        //Dash when space down
        //if (Input.GetKeyDown(KeyCode.Space))
        if (inputController.Gameplay.Dash.triggered)
        {
            Dash();
        }
    }

    private void Move()
    {
        //if magnitude > 1 then round the dirMove because normalized round even if vecto < 1
        if(dirMove.magnitude > 1f)
        {
            dirMove = dirMove.normalized;
        }

        //caculate velocity
        rb.velocity = dirMove * speed;
        //set animation parameter
        animator.SetBool("Run", dirMove.x != 0 || dirMove.y != 0);
    }

    //player flip with mouse direction 
    private void FacingWithMouse()
    {
        //get mouse position
        Vector3 mouPos = Input.mousePosition;
        //get player position
        Vector3 playerScreenLocation = Camera.main.WorldToScreenPoint(transform.position);

        if(mouPos.x < playerScreenLocation.x)
        {
            facingLeft = false;
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            facingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //dash
    private void Dash()
    {
        if (canDash)
        {
            speed += dashForce;
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
        speed -= dashForce;
        //turn off trailRenderer
        trailRenderer.emitting = false;
        //wait time dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
