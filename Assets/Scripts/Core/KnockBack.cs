using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackTime = 0.1f;

    public bool isKnockBack;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockBack(Transform transPlayer)
    {
        isKnockBack = true;
        //get direction knockback
        Vector2 dirKnockBack = (transform.position - transPlayer.position).normalized;
        //add force knockback
        if(rb != null)
        rb.AddForce(knockBackForce * dirKnockBack, ForceMode2D.Impulse);

        StartCoroutine(KnockRoutine());
    }

    //skip time knock
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
    }

}
