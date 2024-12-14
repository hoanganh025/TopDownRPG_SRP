using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private bool canDash = true;
    
    public bool facingLeft = true;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 dirMove;
    private TrailRenderer trailRenderer;

    // singleton
    public static PlayerController InstancePlayer;

    private void Awake()
    {
        //check to avoid duplicate instancePlayer
        if (InstancePlayer == null)
        {
            InstancePlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        animator = rb.GetComponent<Animator>();
        trailRenderer = rb.GetComponent<TrailRenderer>();

        //turn off trailrenderer
        trailRenderer.emitting = false;
    }

    void Update()
    {
        //get direction and vecto
        dirMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        Move();
        FacingWithMouse();

        //Dash when space down
        if (Input.GetKeyDown(KeyCode.Space))
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
