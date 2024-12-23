using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject SlashSwordPrefab;
    [SerializeField] private Transform InsSlashPoint;
    [SerializeField] private float damage;

    private PlayerController playerController;
    private Animator animator;
    private GameObject SlashSword;
    private PolygonCollider2D PolygonCollider2D;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();

        PolygonCollider2D.enabled = false;
    }

    void Update()
    {
        //stop input while time scale = 0
        if (Time.timeScale == 0)
        {
            return;
        }

        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            //when attack enable collider sword
            PolygonCollider2D.enabled = true;
            SlashSword = Instantiate(SlashSwordPrefab, InsSlashPoint.position, Quaternion.identity);
            //set the old tranform.parent to new tranform.parent when to move 
            SlashSword.transform.parent = this.transform.parent;
        }
    }

    //set event to animation slashup
    public void SwingUpAni()
    {
        //flip animation when slash up
        SlashSword.GetComponent<SpriteRenderer>().flipY = true;

        if (playerController.facingLeft == false)
        {
            SlashSword.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    //set event to animation slashdown
    public void SwingDownAni()
    {
        if (playerController.facingLeft == false)
        {
            SlashSword.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    //set collider enable false after attack
    public void DoneAttacking()
    {
        PolygonCollider2D.enabled = false;
    }

    //damaged when hit enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if game object can be damaged, active this
        if(collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.takeDamage(damage);
        }
    }
}
