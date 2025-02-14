using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRangeAttack : MonoBehaviour
{
    public float speed;
    public GameObject skillPrefab;
    public GameObject shootPos;
    public float timeCooldown = 3;
    public float manaConpsumtion = 3;

    private float timer;
    private Rigidbody2D rb;
    private PlayerMana playerMana;

    private void Start()
    {
        playerMana = GetComponent<PlayerMana>();
        playerMana = GetComponentInChildren<PlayerMana>();
        //When start, player can use skill 
        timer = timeCooldown;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RangeAttack();
        }
    }

    private void RangeAttack()
    {
        if(timer > timeCooldown && playerMana.CheckMana(manaConpsumtion))
        {
            playerMana.ConsumptionMana(manaConpsumtion);
            //Get the mouse position 
            Vector3 mouPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouDirection = (mouPos - PlayerController.instance.transform.position).normalized;

            //Convert angle radian to degree
            float angle = Mathf.Atan2(mouDirection.y, mouDirection.x) * Mathf.Rad2Deg;
            //Add rotation to projecttile
            shootPos.transform.rotation = Quaternion.Euler(0, 0, angle);

            //Create projecttile
            GameObject skill = Instantiate(skillPrefab, shootPos.transform.position, Quaternion.Euler(0, 0, angle));
            //Get rigidboy of this projecttile
            rb = skill.GetComponent<Rigidbody2D>();

            //Add force to projecttile
            rb.AddForce(shootPos.transform.right * speed, ForceMode2D.Impulse);

            timer = 0;
        }

    }
}
