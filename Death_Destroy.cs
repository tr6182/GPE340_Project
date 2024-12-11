using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a Health Component
[RequireComponent(typeof(Health))]
public class Death_Destroy : GameAction
{
    [SerializeField] private float delayBeforeDestruction;

    private Health health;

    public override void Start()
    {
        // Get the health component
        health = GetComponent<Health>();

        if (health != null)
        {
            // Register with the OnDie event
            health.OnDeath.AddListener(DestroyOnDeath);
        }
        else
        {
            Debug.LogWarning("Health component not found on this GameObject.", this);
        }
    }

    private void DestroyOnDeath()
    {
        // Destroy the game object with delay
        Debug.Log("oh no " + gameObject.name +" is dead!");
        Destroy(gameObject, delayBeforeDestruction);
    }

    // Unsubscribe from the event to prevent memory leaks
    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath.RemoveListener(DestroyOnDeath);
        }
    }
}
