using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] public float jumpForce = 12;
    [SerializeField] public float moveSpeed = 9;
    [SerializeField] public LayerMask jumpableGround;
    float dirX = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D colliderP;

    private enum movementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colliderP = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        movementState state;

        if (dirX > 0f)
        {
            state = movementState.running;
            sprite.flipX= false;
        }
        else if (dirX < 0f)
        {
            state = movementState.running;
            sprite.flipX= true;
        }
        else
        {
            state = movementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = movementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = movementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(colliderP.bounds.center, colliderP.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
