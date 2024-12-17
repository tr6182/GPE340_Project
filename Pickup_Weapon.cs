using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Weapon : PickUp
{
    public Weapon weaponToEquip; // The weapon to equip when picked up

    public override void OnTriggerEnter(Collider other)
    {
        // Early exit if weapon is not assigned
        if (weaponToEquip == null)
        {
            Debug.LogWarning("No weapon assigned to this pickup.");
            return;
        }

        // Check if the object that triggered the collision is a Pawn
        Pawn thePawn = other.GetComponent<Pawn>();
        if (thePawn != null)
        {
            // Equip the weapon
            thePawn.EquipWeapon(weaponToEquip);

            // Debug log to confirm the weapon has been equipped
            Debug.Log("Weapon Equipped: " + weaponToEquip.name);

            // Destroy the pickup after equipping the weapon
            Destroy(gameObject);
        }
        else
        {
            // Log a message if the collision wasn't with a Pawn
            Debug.LogWarning("The object that triggered the pickup is not a Pawn: " + other.gameObject.name);
        }

        // Call the base class method to retain any functionality implemented there
        base.OnTriggerEnter(other);
    }
}
