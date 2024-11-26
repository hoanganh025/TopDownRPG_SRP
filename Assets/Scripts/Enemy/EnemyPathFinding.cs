using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBackScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBackScript = rb.GetComponent<KnockBack>();
    }

    private void FixedUpdate()
    {
        //when knockback, don't move
        if (knockBackScript.isKnockBack)
        {
            return;
        }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
