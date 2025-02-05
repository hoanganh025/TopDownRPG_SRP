using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossGolem_Arm : Enemy
{
    private Transform playerTransform;
    private Rigidbody2D rbArm;
    [SerializeField] private float armSpeed;
    [SerializeField] private float rotateSpeed;
    private Vector2 direction;

    protected override void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        rbArm = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        direction = (playerTransform.position - transform.position).normalized;

        //Get z of cross product
        //If z < 0, game object is on the right, if z > 0, gameobject is on the left and z = 0 coincides transform.right
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        
        //Rotate this object around pivot by z
        rbArm.angularVelocity = -rotateAmount * rotateSpeed;

        rbArm.velocity = transform.right * armSpeed;
    }

    public override void Death(float currentHeath)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DamageToPlayer(1, playerHeal);
            Destroy(gameObject);
        }
    }
}
