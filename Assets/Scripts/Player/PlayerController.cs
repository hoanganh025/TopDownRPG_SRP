using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector2 dirMove;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = rb.GetComponent<SpriteRenderer>();
        animator = rb.GetComponent<Animator>();
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
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
