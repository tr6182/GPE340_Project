using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_ProjectileShooter : WeaponAction
{
    public float damageDone;
    public float fireRate;
    public float accuracy;

    private float lastShotTime;
    public Transform firePoint;
    public GameObject projectilePrefab;

    public float maxAccuracyRotation;

    public override void Start()
    {
        // Ensure fireRate isn't 0 or negative to avoid potential errors
        if (fireRate <= 0)
        {
            Debug.LogError("Fire rate must be greater than zero.");
            fireRate = 1;  // Set a default value
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

    public void Shoot()
    {
        // Store the direction we shoot without the accuracy system
        Vector3 newFireDirection = firePoint.forward;

        // Get the roation change based on our accuracy
        Quaternion accuracyFireDelta = Quaternion.Euler(0, GetAccuracyRotationDegrees(), 0);

        // Multiply by the rotation from inaccuracy to set new rotation value
        // (Note that multiplication between a Quaternion and Vector is not commutative. The order matters!)
        newFireDirection = accuracyFireDelta * newFireDirection;

        // Calculate time per shot
        float secondsPerShot = 1 / fireRate;

        // Only shoot if enough time has passed
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            

            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Rotate the projectile based on accuracy
            projectile.transform.Rotate(0, GetAccuracyRotationDegrees(), 0);

            // Set the layer for the projectile 
            projectile.layer = this.gameObject.layer;

            // Ensure the projectile has a Projectile component and set its damage
            Projectile projectileData = projectile.GetComponent<Projectile>();
            if (projectileData != null)
            {
                projectileData.damage = damageDone;
            }
            else
            {
                Debug.LogWarning("Projectile does not have a Projectile component!");
            }

            // Update the last shot time
            lastShotTime = Time.time;
        }
    }
}

