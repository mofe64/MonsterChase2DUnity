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
    private Rigidbody2D playerBody;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private string WALK_ANIMATION = "walk";

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
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
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
}
