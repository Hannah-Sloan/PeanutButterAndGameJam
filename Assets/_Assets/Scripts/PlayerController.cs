using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    [Range(0, 1)]
    [SerializeField] private float groundCheckDist;

    [Space(10)]

    [Range(0, 10)]
    [SerializeField] private float moveSpeed;
    [Range(0,10)]
    [SerializeField] private float jumpVelocity;
    //[Range(1,3)]
    [SerializeField] private float gravityFallMultiplier;
    //[Range(1, 3)]
    [SerializeField] private float lowJumpGravityMultiplier;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) 
        {
            rigidbody.velocity += Vector2.up * jumpVelocity;
        }

        //Left Right
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = Vector2.left * moveSpeed + Vector2.up * rigidbody.velocity;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = Vector2.right * moveSpeed + Vector2.up * rigidbody.velocity;
        }

        rigidbody.gravityScale = 1;

        // fall hard
        if (rigidbody.velocity.y < 0 && !IsGrounded())
        {
            rigidbody.gravityScale = gravityFallMultiplier;
        }

        // hold to jump higher
        if (rigidbody.velocity.y >= 0 && !Input.GetKey(KeyCode.Space)) 
        {
            rigidbody.gravityScale = lowJumpGravityMultiplier;
        }
    }

    private bool IsGrounded()
    {
        Color col;
        var check1 = Physics2D.Raycast(transform.position, -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        col = check1 ? Color.green : Color.red;
        Debug.DrawRay(transform.position, -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        var check2 = Physics2D.Raycast(transform.position + (transform.right * collider.bounds.extents.x), -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        col = check2 ? Color.green : Color.red;
        Debug.DrawRay(transform.position + (transform.right * collider.bounds.extents.x), -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        var check3 = Physics2D.Raycast(transform.position + (transform.right * -collider.bounds.extents.x), -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        col = check3 ? Color.green : Color.red;
        Debug.DrawRay(transform.position + (transform.right * -collider.bounds.extents.x), -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        return check1 || check2 || check3;
    }
}
