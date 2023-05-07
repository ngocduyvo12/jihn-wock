using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStates { Walk, Attack, Dead }
public class BossScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMovement player;
    Animator animator;
    
    [Header("Settings")]
    [SerializeField] private float minDistToShoot = 7f;
    [SerializeField] private float maxDistToCoolDown = 13f;
    [SerializeField] private float walkSpeed = 3f;

    [Header("Movements")]
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform pointToMoveTowards;
    Vector3 directionOfPlayer;

    [Header("Debugs")]
    [SerializeField] private BossStates states;
    [SerializeField] float distFromPlayer;
    [SerializeField] float distFromPoint;

    public float health = 100f;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        pointToMoveTowards = point1;
    }

    private void Update()
    {
        if (states != BossStates.Dead)
        {
            GetPointToMove();
            CalculateDistance();
            HandleStates();



            if (health <= 0f)
            {
                animator.SetTrigger("Dead");
                animator.SetBool("Shoot", false);
                animator.SetBool("Walk", false);
                GameManager.WON = true;
                Destroy(gameObject, 2f);
                states = BossStates.Dead;
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
            states = BossStates.Attack;
        }
        else if (distFromPlayer >= maxDistToCoolDown)
        {
            states = BossStates.Walk;
        }
    }

    void HandleStates()
    {
        switch (states)
        {
            case BossStates.Walk:

                //Walk Enemy

                if (pointToMoveTowards == point1)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 170f, 0f));
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
                transform.position = Vector3.MoveTowards(transform.position, pointToMoveTowards.position, walkSpeed * Time.deltaTime);
                animator.SetBool("Walk", true);
                animator.SetBool("Shoot", false);

                break;
            case BossStates.Attack:

                Attack();

                break;
            case BossStates.Dead:
                break;
        }
    }

    void Attack()
    {
        directionOfPlayer = Vector3.Normalize(player.transform.position - transform.position);
        float angle = Mathf.Atan2(directionOfPlayer.y, directionOfPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= 0.1f)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Shoot", true);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, walkSpeed * Time.deltaTime);
            animator.SetBool("Walk", true);
            animator.SetBool("Shoot", false);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");
    }
}