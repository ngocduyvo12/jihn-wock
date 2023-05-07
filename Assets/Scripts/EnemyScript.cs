using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyStates { Walk, Shoot, Dead }
public class EnemyScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform medKit;
    SpriteRenderer spriteRenderer;
    Animator animator;

    [Header("Settings")]
    [SerializeField] private float minDistToShoot = 5f;
    [SerializeField] private float maxDistToCoolDown = 15f;
    [SerializeField] private float walkSpeed = 3f;

    [Header("Shooting")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform bullet;
    private float shootTime = 0f;
    [SerializeField] private float nextTimeToShoot = 1f;


    [Header("Movements")]
    private Transform point1;
    private Transform point2;
    [SerializeField] private Transform pointToMoveTowards;
    Vector3 directionOfPlayer;


    bool isGrounded = false;

    public float health = 100f;

    [Header("Debugs")]
    [SerializeField] private EnemyStates states;
    [SerializeField] float distFromPlayer;
    [SerializeField] float distFromPoint;
    private void Start()
    {
        states = EnemyStates.Walk;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerMovement>();
        point1 = transform.parent.Find("Point1");
        point2 = transform.parent.Find("Point2");
        pointToMoveTowards = point1;
        StartCoroutine(WaitToGround());
    }

    private void Update()
    {
        if (states != EnemyStates.Dead)
        {
            GetPointToMove();
            CalculateDistance();
            HandleStates();



            if (health <= 0f)
            {
                animator.SetTrigger("Dead");
                animator.SetBool("Shoot", false);
                animator.SetBool("Walk", false);
                SpawnMedKit();
                Destroy(transform.parent.gameObject, 2f);
                states = EnemyStates.Dead;
            }
        }
    }

    void GetPointToMove()
    {
        distFromPoint = Vector3.Distance(transform.position, pointToMoveTowards.position);
        if (distFromPoint < 0.02f)
        {
            if (pointToMoveTowards == point1)
            {
                pointToMoveTowards = point2;
            }
            else
            {
                pointToMoveTowards = point1;
            }
        }
    }

    void CalculateDistance()
    {
        distFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distFromPlayer <= minDistToShoot)
        {
            states = EnemyStates.Shoot;
        }
        else if (distFromPlayer >= maxDistToCoolDown)
        {
            states = EnemyStates.Walk;
        }
    }

    void HandleStates()
    {
        switch (states)
        {
            case EnemyStates.Walk:

                //Walk Enemy

                if (pointToMoveTowards == point1)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                transform.position = Vector3.MoveTowards(transform.position, pointToMoveTowards.position, walkSpeed * Time.deltaTime);
                animator.SetBool("Walk", true);
                animator.SetBool("Shoot", false);

                break;
            case EnemyStates.Shoot:

                Shoot();
                animator.SetBool("Walk", false);
                animator.SetBool("Shoot", true);

                break;
            case EnemyStates.Dead:
                break;
        }
    }

    void Shoot()
    {
        directionOfPlayer = Vector3.Normalize(player.transform.position - transform.position);
        float angle = Mathf.Atan2(directionOfPlayer.y, directionOfPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        shootTime += Time.deltaTime;
        if (shootTime >= nextTimeToShoot)
        {
            AudioManager.instance.EnemyShoot();
            GameObject projectile = Instantiate(bullet.gameObject, shootPoint.position, Quaternion.identity);
            projectile.GetComponent<EnemyBullet>().direction = directionOfPlayer;
            shootTime = 0f;
        }
    }

    void SpawnMedKit()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Instantiate(medKit.gameObject, spawnPos, Quaternion.identity);
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
        AudioManager.instance.Hurt();
    }
    IEnumerator WaitToGround()
    {
        yield return new WaitForSeconds(0.5f);
        //GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }
}