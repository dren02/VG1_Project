using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{ 
    private Rigidbody2D player;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private bool isDashing = false;
    public float dashDuration = 0.2f;
    private float nextDashTime = 0f;
    private float dashEndTime;
    public int jumpsLeft;
    
    // Use this for initialization
    void Start () 
    {
        player = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update() 
    { 
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); 

        if (isTouchingGround)
        {
            jumpsLeft = 2;  // Reset jumps when touching the ground
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpsLeft > 0) 
        {
            jumpsLeft--;
            player.AddForce(Vector2.up * 14f, ForceMode2D.Impulse);
        }
        // move left & right
        if (!isDashing)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                player.AddForce(Vector2.left * 25f * Time.deltaTime, ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                player.AddForce(Vector2.right * 25f * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        // dash when space bar and left/right arrow are pressed
        if (Time.time >= nextDashTime && isTouchingGround && Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                StartDash(Vector2.left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                StartDash(Vector2.right);
            }
        }
        // stop dash after duration
        if (isDashing && Time.time >= dashEndTime)
        {
            isDashing = false;
            player.velocity = Vector2.zero;
        }
    }

    void StartDash(Vector2 direction)
    {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        nextDashTime = Time.time + 1f;
        player.velocity = direction * 30f;
    }
    
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.7f);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    jumpsLeft = 2;
                }
            }
        }
    }
}

