using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] float rayDistance = 1f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private float initialShootPointPosX;


    //References
    private Rigidbody2D rb;
    private JihnWock inputActions;

    //Input Actions
    private InputAction moveControls;
    private InputAction jumpControl;

    //Private Variables
    private int jumpCount = 0;
    bool makeGameOver = true;
    [SerializeField] private bool isGrounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new JihnWock();
    }

    private void OnEnable()
    {
        inputActions = new JihnWock();
        moveControls = inputActions.Player.Move;
        moveControls.Enable();

        jumpControl = inputActions.Player.Jump;
        jumpControl.Enable();
        jumpControl.performed += Jump;
    }

    private void OnDisable()
    {
        moveControls.Disable();
        jumpControl.Disable();
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, -Vector2.up, 0.1f);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        if (PlayerHealth.isDead)
            return;
        Move();
        if (transform.position.y <= -8.0f)
        {
            if (makeGameOver)
            {
                GameManager.LOST = true;
                makeGameOver = false;
            }
        }
        if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else
        {
            animator.SetBool("Jump", true);
        }
    }

    private void Move()
    {
        Vector2 moveDir = moveControls.ReadValue<Vector2>();
        Vector2 move = new Vector2(moveDir.x, 0f);
        transform.Translate(move * movementSpeed * Time.fixedDeltaTime, Space.World);
        if (move.x > 0f)
        {
            animator.SetLayerWeight(0, 1f);
            animator.SetLayerWeight(1, 0f);
            shootPoint.localPosition = new Vector3(initialShootPointPosX, 0.028f, shootPoint.position.z);
            shootPoint.rotation = Quaternion.identity;
        }
        else if (move.x < 0f)
        {
            animator.SetLayerWeight(0, 0f);
            animator.SetLayerWeight(1, 1f);
            shootPoint.localPosition = new Vector3(-0.144f, 0.028f, shootPoint.position.z);
            shootPoint.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }    
        if (move.x != 0f && isGrounded)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            AudioManager.instance.Jump();
            Vector2 jumpDir = new Vector2(0f, jumpForce);
            rb.AddForce(jumpDir, ForceMode2D.Impulse);
        }
    }
    private void Crouch(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            GetComponent<BoxCollider2D>().size = new Vector2();
        }
    }


    #region COLLISIONS

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        //    isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MedKit"))
        {
            AudioManager.instance.MedKit();
            GetComponent<PlayerHealth>().health += 15;
            GetComponent<PlayerHealth>().UpdateHealthBar();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Floor"))
        //{
        //    isGrounded = false;
        //}
    }


    #endregion

}
