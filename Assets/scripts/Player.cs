using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int jumpForce;
    public int health;
    public Transform groundCheck;
    private bool invunerable = false;
    private bool grounded = false;
    private bool jumping = false;
    private bool facingRight = true;
    private SpriteRenderer sprite;
    private Rigidbody2D rb2d;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // trate the jump
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));


        // trate jump button
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        /**
        How is a vector that expects two x,y positions
        x = should be the movement (move * speed) for velocity 
        y = velocity it has at the moment it is jumping or falling keeping its velocity on the "y" axis that is
        */
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
        /*
        If the player moves to the left side, it is necessary to make him turn around so it doesn't look like he's moving backwards.
        */
        if ((move < 0f && facingRight) || (move > 0f && !facingRight))
        {
            Flip();
        }

        // add force to jump
        if (jumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
