using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float movementSpeed;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer rend;

    [SerializeField] Transform[] groundChecks;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckHeight;

    bool grounded;

    void Start()
    {
        //we get all the necessary components at the start of the game
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        grounded = CheckIfGrounded();
        Movement();
    }

    void Movement()
    {
        //Getting Left & Right input from WASD. Arrows, joysticks etc.
        float horizontal = Input.GetAxis("Horizontal");
        //setting movement speed by multiplying the imput with speed
        rb.linearVelocityX = horizontal * movementSpeed;

        //getting current speed as an absolute (always positive) value so that we can do only one comparison check
        float currentSpeed = Mathf.Abs(rb.linearVelocityX);

        //checking if speed is more than a treshold of 0.1f
        //and then setting the animator value "moving" to true or false so that it can play the correct animation
        if(currentSpeed > 0.1f)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        //flipping the character texture if moving left or right.
        if(rb.linearVelocityX < -0.1f)
        {
            rend.flipX = true;
        }
        if(rb.linearVelocityX > 0.1f)
        {
            rend.flipX = false;
        }
    }


    //Happens every time we collide with a 2D collider that is marked as trigger.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //we check if the gameobject we collide with has a Goal tag.
        if(collision.gameObject.GetComponent<Trophy>())
        {
            if (!GameManager.instance.MissionComplete()) return;
            WinGame();
        }
    }

    void WinGame()
    {
        //we reset player animation states and disable player script when we reach the goal.
        rb.linearVelocityX = 0;
        anim.SetBool("Win", true);
        anim.SetBool("Moving", false);
        anim.SetBool("Grounded", true);
        this.enabled = false;
    }

    /// <summary>
    /// We check if player is standing on ground.
    /// </summary>
    /// <returns></returns>
    bool CheckIfGrounded()
    {
        bool hits = false;
        for (int i = 0; i < groundChecks.Length; i++)
        {
            Debug.DrawRay(groundChecks[i].position, -groundChecks[i].up * groundCheckHeight);
            RaycastHit2D hit = Physics2D.Raycast(groundChecks[i].position, -groundChecks[i].up, groundCheckHeight, groundLayer);
            if (hit)
            {
                hits = true;
                continue;
            }
        }
        return hits;
    }
}
