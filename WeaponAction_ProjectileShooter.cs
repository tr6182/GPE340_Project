using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction_ProjectileShooter : WeaponAction
{
    public float damageDone;
    public float fireRate;

    private float lastShotTime;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private void Start()
    {
        // Ensure fireRate isn't 0 or negative to avoid potential errors
        if (fireRate <= 0)
        {
            Debug.LogError("Fire rate must be greater than zero.");
            fireRate = 1;  // Set a default value
        }
    }

    public void Shoot()
    {
        // Calculate time per shot
        float secondsPerShot = 1 / fireRate;

        // Only shoot if enough time has passed
        if (Time.time >= lastShotTime + secondsPerShot)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

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
