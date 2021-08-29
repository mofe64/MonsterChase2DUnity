using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   //serialize field forces unity to expose private variable in the inspector panel, but these fields are stil private
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;
    private float movementX;
    [SerializeField]
    private bool isGrounded = true;
    private Rigidbody2D playerBody;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private string WALK_ANIMATION = "walk";
    private string GROUND_TAG = "Ground";
    private string MONSTER_TAG = "Monster";

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //update called every frame if mono behavior is enabled
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();

        PlayerJump();
    }


    /* fixed update is called every fixed frame rate 
        usually used to perform physics calc
        fixed update can run once, zero or several times per fram depending on how many physics frames per second are set in the projects time settings and how fats/slow the
        frame rate is
        used when applying physics realated functions, because we know it will be executed exactly in sync with the physics engine itself unlike update
    */
    private void FixedUpdate()
    {

    }
    void PlayerMoveKeyboard()
    {
        /*
            GetAxis is smoothed based on the sensitivity setting so that the value changes from 0 to 1, or to -1,
            GetAxisRaw will only ever return 0,-1, or 1
        */
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            //movement right
            //reset sprite renderer flip prop to make sprite face original direction
            spriteRenderer.flipX = false;
            anim.SetBool(WALK_ANIMATION, true);
        }
        else if (movementX < 0)
        {
            //movement left
            //we use the sprite renderer flip prop to enure sprite is facing the right direction
            spriteRenderer.flipX = true;
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            //idle 
            anim.SetBool(WALK_ANIMATION, false);
        }

    }

    void PlayerJump()
    {
        //returns true if platform jump button is pressed (Platform neutral)
        //Get button down is triggered when button is pressed down
        //Get Button up is triggered when button is released
        //Get Button returns true while the button is pressed down
        /*
        NOTE ----> When using getKeyDown or getButtonDown dont call un fixed update, call in update instead, as there is a high tendency that the fixedUpdate for the frame that 
        contains the keypress/button press will miss the command
        -------> instantaenous input (GetKeyUp,Down,ButtonUp,Down, MouseButtonUp, Down) should be done in update while sustainedInput (GetKey, GetButton, GetMouseButton)
        shouuld be read from where they're being used. eg for playermovement with phsyics should be done in FixedUpdate

        */
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            /*
                Add force applies a force to the rigidbody of gameobject 
                In general the result of addforce is a chaneg in velocity of game obj (is acceleration)
                ForceMode2d.impulse denotes an instant force
                force is applied to the rigid body via the objects mass
            */
            isGrounded = false;
            playerBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            //Note ---> sprite falling over problem ? in rigid body--> constraints ---> Freeze rotation on Z
        }
    }

    //Detect collisions between game objects, other param is second obj we are colliding with
    // called automatically on collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        //compare if out player game obj has collided with the game obj with tag Ground
        if (other.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag(MONSTER_TAG))
        {
            Destroy(gameObject);
        }
    }


    //Detects collisions with trigger colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(MONSTER_TAG))
        {
            Destroy(gameObject);
        }
    }
}
