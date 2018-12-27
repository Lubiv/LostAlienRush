using UnityEngine;

public class PlayerController : Creature
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)    
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));

        if (flipSprite)
            spriteRenderer.flipX = !spriteRenderer.flipX;

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", Mathf.Abs(velocity.y) / maxSpeed);


        targetVelocity = move * maxSpeed;
    }
}
