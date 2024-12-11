using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Weapon : PickUp
{
    public Weapon weaponToEquip; // The weapon to equip when picked up

    public override void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collision is a Pawn
        Pawn thePawn = other.GetComponent<Pawn>();
        if (thePawn != null && weaponToEquip != null)
        {
            // Equip the weapon
            thePawn.EquipWeapon(weaponToEquip);
            // debug log to say which weapon has been equiped
            Debug.Log("Weapon Equipped: " + weaponToEquip.name); 

            // Destroys the pickup after equipping the weapon
            Destroy(gameObject); 
        }
        else if (weaponToEquip == null)
        {
            Debug.LogWarning("No weapon assigned to this pickup.");
        }

        // Call the base class method to retain any functionality implemented there
        base.OnTriggerEnter(other);
    }
}
