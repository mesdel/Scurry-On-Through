using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [Range(0, .3f)] [SerializeField] private float moveSmoothing = .05f;
    [SerializeField] private LayerMask whatIsGround;   // A mask determining what is ground to the character
    [SerializeField] private Transform groundCheck;    // A position marking where to check if the player is grounded.
    const float groundedRadius = .025f; // Radius of the overlap circle to determine if grounded

    public bool isGrounded;
    public bool jump = false;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovement();
        jump = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if(!wasGrounded)
                {
                    // insert landing effects here
                }
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 targetVelocity = new Vector3(horizontalInput * speed, playerRb.velocity.y);
        playerRb.velocity = Vector3.SmoothDamp(playerRb.velocity, targetVelocity, ref velocity, moveSmoothing);

        if (jump && isGrounded)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
            isGrounded = false;
            Debug.Log("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Hazard"))
        {
            // take damage
            Debug.Log("damage");
        }
    }
}
