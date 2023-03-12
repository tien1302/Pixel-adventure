using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float moveSpeed = 500f;
    public float maxSpeed = 8f;
    public float idleFriction = 0.9f;

    public Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Collider2D swordCollider;

    Vector2 moveInput = Vector2.zero;

    bool isMoving = false;
    bool canMove = true;

    [SerializeField] private AudioSource attackSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!PauseMenu.isPaused)
        {
            if (canMove == true && moveInput != Vector2.zero)
            {
                //rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);
                rb.AddForce(moveInput * moveSpeed * Time.deltaTime);

                if (rb.velocity.magnitude > maxSpeed)
                {
                    float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                    rb.velocity = rb.velocity.normalized * limitedSpeed;
                }

                if (moveInput.x > 0)
                {
                    spriteRenderer.flipX = false;
                    gameObject.BroadcastMessage("IsFacingRight", true);
                }
                else if (moveInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                    gameObject.BroadcastMessage("IsFacingRight", false);
                }
                IsMoving = true;
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
                IsMoving = false;
            }
        }
        
    }



    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnFire()
    {
        if (!PauseMenu.isPaused)
        {
            animator.SetTrigger("swordAttack");
            attackSound.Play();
        }
    }

    void LockMovement()
    {
        canMove = false;
    }

    void UnlockLockMovement()
    {
        canMove = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
