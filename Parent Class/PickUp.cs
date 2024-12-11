using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Anything that uses this script requires a collider
[RequireComponent(typeof(Collider))]
public class PickUp : MonoBehaviour
{
    // A hidden collider component can't be seen in inspector
    private Collider colliderComponent;

    // UnityEvent to handle pickup logic
    public UnityEvent OnPickup;

    public void Awake()
    {
        // Load the collider
        colliderComponent = GetComponent<Collider>();

        // Set it to be a trigger
        colliderComponent.isTrigger = true;

        // Ensures OnPickup event is assigned
        if (OnPickup == null)
        {
            Debug.LogWarning("OnPickup UnityEvent is not assigned in the inspector.");
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player or a valid object 
        if (other.CompareTag("Player"))  // Assuming the player has a tag "Player"
        {
            // Optional: You could add a delay before destroying, if needed
            Destroy(gameObject, 0.1f);  // Delay destruction for 0.1 seconds (or adjust when needed)

            // Invoke the event so designers can add GameActions
            OnPickup.Invoke();
        }
    }
}
