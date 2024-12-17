using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float gizmoBoxHeight = 2; // Our gizmo box is 2 units tall
    private float gizmoBoxWidth = 1; // Our gizmo box is 2 units tall

    public void OnDrawGizmos()
    {
        // Create a transparent yellow to use
        Color boxColor = Color.yellow;
        boxColor.a = 0.7f;  // Set the transparency for the gizmo
        Gizmos.color = boxColor;

        // Since our box's position is the CENTER of the box, we will want to draw the box 1/2 the height of the box off the ground.
        Vector3 boxPosition = transform.position;
        boxPosition += Vector3.up * (gizmoBoxHeight / 2);  // Offset it upwards to show the box's center

        // Our size will be square on the X/Z, and only use the height on Y-axis
        Vector3 boxSize = new Vector3(gizmoBoxWidth, gizmoBoxHeight, gizmoBoxWidth);
        Gizmos.DrawCube(boxPosition, boxSize);

        // Now, set the gizmo color to red for our ray that shows direction
        Gizmos.color = Color.red;
        // Draw a ray in the direction of our spawn (using transform.forward)
        Gizmos.DrawRay(boxPosition, transform.forward * 5); // Adjusted length of the ray to make it more visible
    }
}
