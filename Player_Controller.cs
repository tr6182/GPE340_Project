using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player_Controller inherits the MakeDescisions from the Controller class
public class Player_Controller : Controller
{

    // bool is a true or false statement
    public bool isMouseRotation;

    public float lives;

    // Update is called once per frame
    protected override void Update()
    {
        // Do what every controller does (Make Decisions)
        base.Update();
    }

    protected override void MakeDecisions()
    {
        // Get the Input axes
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Limit the vector to a max magnitude of 1
        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        // Tell the pawn to move
        pawn.Move(moveVector);

        // Tell the pawn to rotate based on the CameraRotation axis
        pawn.Rotate(Input.GetAxis("CameraRotation"));

        // If we are mouse rotating
        if (isMouseRotation)
        {
            // Make sure Camera.main is not null before using it
            if (Camera.main != null)
            {
                // Create the Ray from the mouse position in the direction the camera is facing.
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Create a plane at the pawn's feet with a normal (perpendicular direction) of world up.
                Plane footPlane = new Plane(Vector3.up, pawn.transform.position);

                // Find the distance down the ray where the plane and ray intersect.
                float distanceToIntersect;

                // Raycast to check if the mouseRay intersects the footPlane
                if (footPlane.Raycast(mouseRay, out distanceToIntersect))
                {
                    // Find the intersection point of the ray and the plane
                    Vector3 intersectionPoint = mouseRay.GetPoint(distanceToIntersect);

                    // Make the pawn rotate to face the intersection point
                    pawn.RotateToLookAt(intersectionPoint);
                }
                else
                {
                    // Handle the case where the ray doesn't intersect the plane
                    Debug.Log("Camera is not looking at the ground - no intersection between plane and ray.");
                }
            }
            else
            {
                // Handle the case where Camera.main is null
                Debug.LogError("Camera.main is null. Make sure the main camera is tagged correctly.");
            }
        }
        else
        {
            // If not mouse rotating, rotate the pawn based on the CameraRotation axis
            pawn.Rotate(Input.GetAxis("CameraRotation"));
        }

        // Call Weapon Events on FireButton presses
        if (Input.GetButtonDown("Fire1"))
        {
            pawn.weapon.OnPrimaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            pawn.weapon.OnPrimaryAttackEnd.Invoke();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            pawn.weapon.OnSecondaryAttackBegin.Invoke();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            pawn.weapon.OnSecondaryAttackEnd.Invoke();
        }

    }

    
}