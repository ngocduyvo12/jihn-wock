using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 10f;
    PlayerMovement player;
    Rigidbody2D rb;
    bool left = false;
    bool right = false;
    bool once = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (!player.transform.Find("GFX").GetComponent<SpriteRenderer>().flipX)
        {
            if (once)
            {
                right = true;
                once = false;
            }
        }
        else
        {
            if (once)
            {
                left = true;
                once = false;
            }
        }
        if (right)
        {
            rb.velocity = Vector3.right * moveSpeed;
        }
        else if(left)
        {
            rb.velocity = Vector3.left * moveSpeed;
        }
        //transform.Translate(Vector3.right * moveSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMove = collision.GetComponent<PlayerMovement>();
        if (collision.gameObject != playerMove)
        {
            Destroy(gameObject);
        }
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        BossScript boss = collision.gameObject.GetComponent<BossScript>();
        if (boss != null)
        {
            boss.TakeDamage(damage / 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement playerMove = collision.gameObject.GetComponent<PlayerMovement>();
        if (collision.gameObject != playerMove)
        {
            Destroy(gameObject);
        }
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        BossScript boss = collision.gameObject.GetComponent<BossScript>();
        if (boss != null)
        {
            boss.TakeDamage(damage / 2);
        }
    }
}