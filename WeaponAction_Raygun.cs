using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float accuracy;

    public float maxAccuracyRotation;

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

    public virtual float GetAccuracyRotationDegrees(float accuracyModifier = 1)
    {
        // Return a random percentage between min and max AccuracyRoation 
        // Get a random number between 0 and 1 ( a percentage )
        float accuracyDeltaPercentage = Random.value;

        // Find that percentage between the negative (to the Left) and positive (to the right) values of this rotation.
        float accuracyDeltaDegrees = Mathf.Lerp(-maxAccuracyRotation, maxAccuracyRotation, accuracyDeltaPercentage);
        accuracyDeltaDegrees *= accuracyModifier;

        // Return that value
        return accuracyDeltaDegrees;
    }

    // Shoot method to handle raycast and damage
    public void Shoot()
    {
        // Store the direction we shoot without the accuracy system
        Vector3 newFireDirection = firepoint.forward;

        // Get the rotation change based on our accuracy
        Quaternion accuracyFireDelta = Quaternion.Euler(0, GetAccuracyRotationDegrees(), 0);

        // Multiply by the rotation from inaccuracy to set new rotation value
        newFireDirection = accuracyFireDelta * newFireDirection;

        // Variable to hold our raycast hit data
        RaycastHit hit;

        // Check if it is time to fire the weapon
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            // Visualize the ray (for debugging or feedback in-game)
            Debug.DrawRay(firepoint.position, newFireDirection * fireDistance, Color.red);

            // Perform the Raycast
            if (Physics.Raycast(firepoint.position, newFireDirection, out hit, fireDistance))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

                // If we hit an object with a Health component
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                if (otherHealth != null)
                {
                    // Tell it to take damage!
                    otherHealth.TakeDamage(weapon.damageDone);
                }

                // Optional: You can add visuals here if needed (such as drawing a line)
                if (lineRenderer != null)
                {
                    lineRenderer.SetPosition(0, firepoint.position);
                    lineRenderer.SetPosition(1, hit.point);
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

