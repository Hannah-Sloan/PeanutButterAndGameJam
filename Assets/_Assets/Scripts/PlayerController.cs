using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    private float groundCheckDist = 0.3f;

    [Header("Refs")]
    [SerializeField] private SpriteRenderer sprite;


    [Space(10)]


    [Header("Move")]

    [Range(10f, 50f)]
    [SerializeField] private float accelerateSpeed;
    [Range(10f, 100f)]
    [SerializeField] private float decelerateSpeed;
    [Range(1, 10)]
    [SerializeField] private float maxSpeed;


    [Space(10)]


    [Header("Jump")]

    [Range(0,10)]
    [SerializeField] private float jumpVelocity;
    [Range(1,10)]
    [SerializeField] private float gravityFallMultiplier;
    [Range(1, 10)]
    [SerializeField] private float lowJumpGravityMultiplier;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        //Left Right
        float moveX = Input.GetAxis("Horizontal");
        //Debug.DrawRay(transform.position, moveX * Vector2.right * maxSpeed, Color.blue);

        if ((Mathf.Sign(rigidbody.velocity.x) == Mathf.Sign(moveX) || rigidbody.velocity.x == 0) && moveX != 0)
        {
            rigidbody.velocity += Vector2.right * Mathf.Sign(rigidbody.velocity.x) * Mathf.Min(accelerateSpeed * Time.deltaTime, maxSpeed - Mathf.Abs(rigidbody.velocity.x));
            sprite.color = Color.yellow;
        }
        else 
        {
            if (decelerateSpeed * Time.deltaTime > Mathf.Abs(rigidbody.velocity.x))
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            else
                rigidbody.velocity += Vector2.right * -Mathf.Sign(rigidbody.velocity.x) * decelerateSpeed * Time.deltaTime;

            sprite.color = Color.yellow;
        }


        if (Mathf.Approximately(Mathf.Abs(rigidbody.velocity.x), 0))
            sprite.color = Color.red;
        if (Mathf.Approximately(Mathf.Abs(rigidbody.velocity.x), maxSpeed))
            sprite.color = Color.green;

        print(Mathf.Abs(rigidbody.velocity.x)/maxSpeed);

        //Jump
        //if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        //{
        //    rigidbody.velocity += Vector2.up * jumpVelocity;
        //}

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

        if (IsGrounded())
            rigidbody.gravityScale = 1;
    }

    private bool IsGrounded()
    {
        //Color col;
        var check1 = Physics2D.Raycast(transform.position, -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        //col = check1 ? Color.green : Color.red;
        //Debug.DrawRay(transform.position, -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        var check2 = Physics2D.Raycast(transform.position + (transform.right * collider.bounds.extents.x), -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        //col = check2 ? Color.green : Color.red;
        //Debug.DrawRay(transform.position + (transform.right * collider.bounds.extents.x), -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        var check3 = Physics2D.Raycast(transform.position + (transform.right * -collider.bounds.extents.x), -Vector2.up, collider.bounds.extents.y + groundCheckDist);
        //col = check3 ? Color.green : Color.red;
        //Debug.DrawRay(transform.position + (transform.right * -collider.bounds.extents.x), -Vector3.up * (collider.bounds.extents.y + groundCheckDist), col);

        return check1 || check2 || check3;
    }
}
