using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // since it's labeled as public it can be seen and changed in the unity inspector 
    // the location of the target
    public Transform target;
    // the speed at which the camera moves in
    public float speed;
    // the distance of the camera from the pawn
    public float distance;


    // Update is called once per frame
    void Update()
    {
        // Calculate where our camera wants to be
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + distance, target.position.z);

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Look at the target
        transform.LookAt(target.position, target.forward);

        

    }
}
