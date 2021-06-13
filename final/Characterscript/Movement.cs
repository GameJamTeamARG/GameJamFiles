using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movement_speed;
    public float jump_height;
    public float gravity;

    private Vector3 movement_of_player = Vector3.zero;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");
        
        Vector3 player_walking = xMovement * transform.right + zMovement * transform.forward;

        //float xMovement_speed = player_walking.x * movement_speed;
        //float zMovement_speed = player_walking.z * movement_speed;

        movement_of_player = Vector3.zero;

        movement_of_player += player_walking;
        
        float xMovement_speed = movement_of_player.x * movement_speed;
        float zMovement_speed = movement_of_player.z * movement_speed;

        

        movement_of_player = new Vector3(xMovement_speed, movement_of_player.y, zMovement_speed);



        movement_of_player.y -= gravity * Time.deltaTime;
        controller.Move(movement_of_player * Time.deltaTime);
        
        
    }
}
