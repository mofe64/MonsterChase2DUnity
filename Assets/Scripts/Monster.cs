using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [HideInInspector]
    public float speed;
    private Rigidbody2D monsterBody;
    // Start is called before the first frame update
    private void Awake()
    {
        monsterBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //velocity is linear velocity of rigid body in units per seconds
        //veleociity can be thought off as a force pushing the player to move in any direction
        //below we are adding our speed var to x velocity which will push our game obj to move left and right, we keep the y velocity as same value
        monsterBody.velocity = new Vector2(speed, monsterBody.velocity.y);

    }
}
