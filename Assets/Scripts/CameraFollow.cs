using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPosition;

    //set bounds for the camera
    [SerializeField]
    private float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {
        //get the player game obj's transfrom component
        player = GameObject.FindWithTag("Player").transform;
    }

    //Late Update is called every frame, but its called only after all calcs in update are finished
    //This is perfect for allowing the camera to follow the player because it will be called after the player's position has been changed 
    //player animations have been rendered
    void LateUpdate()
    {
        //get current position of the camera
        tempPosition = transform.position;
        //set x prop on camera position to be equal to player game obj x prop
        tempPosition.x = player.position.x;
        //watch for camera bounds
        if (tempPosition.x < minX)
        {
            tempPosition.x = minX;
        }
        if (tempPosition.x > maxX)
        {
            tempPosition.x = maxX;
        }
        //update current camera position
        transform.position = tempPosition;

    }
}
