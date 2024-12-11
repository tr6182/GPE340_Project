using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a child of another child class
public class WeaponAction_Raygun : WeaponAction
{
    // How far an enemy has to be to take damage
    public float fireDistance;

    // Where we are firing from
    public Transform firepoint;

    // A switch to allow for automatic fire or semi-auto fire
    private bool isAutofireActive;

    // Makes sure we store the last shot that has fired
    private float lastShotTime;

    // Time between shots (for better readability and performance)
    private float secondsPerShot;

    // LineRenderer to visualize the ray (optional)
    private LineRenderer lineRenderer;

    // Awake is called when the script is initialized
    public override void Awake()
    {
        base.Awake();
        // Initialize the fire rate
        secondsPerShot = 1 / weapon.fireRate;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        lineRenderer = GetComponent<LineRenderer>(); // If you want to visualize the ray with LineRenderer
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        // Handle Autofire
        if (isAutofireActive)
        {
            Shoot();
        }
        else
        {
            // Handle Semi-Auto Fire
            if (Input.GetButtonDown("Fire1")) // "Fire1" is Unity's default fire button (can be re-mapped)
            {
                Shoot();
            }
        }
    }

    // Shoot method to handle raycast and damage
    public void Shoot()
    {
        // Variable to hold our raycast hit data
        // raycast requires a mesh collider
        RaycastHit hit;

        // Check if it is time to fire the weapon
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            // Visualize the ray (for debugging or feedback in-game)
            Debug.DrawRay(firepoint.position, firepoint.forward * fireDistance, Color.red);

            // Perform the Raycast
            if (Physics.Raycast(firepoint.position, firepoint.forward, out hit, fireDistance))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                // If we hit an object with a Health component
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                if (otherHealth != null)
                {
                    // Tell it to take damage!
                    otherHealth.TakeDamage(weapon.damageDone);
                }
            }

            // Save the time when the shot was fired
            lastShotTime = Time.time;
        }
    }

    // Begin Autofire (called externally to activate autofire)
    public void AutofireBegin()
    {
        isAutofireActive = true;
    }

    // End Autofire (called externally to deactivate autofire)
    public void AutofireEnd()
    {
        isAutofireActive = false;
    }
}
