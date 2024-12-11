using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Pawn Class is the parent class 
public abstract class Pawn : MonoBehaviour
{
    // allows us to apply a controller to our pawn
    public Controller controller;

    // movement speed of our pawn
    public float maxMoveSpeed;

    // allow us to apply a weapon to our pawn
    public Weapon weapon;

    // the transform of the weapon attachment point
    public Transform weaponAttachmentPoint;

    // this'll allow is to equip a weapon
    public void EquipWeapon(Weapon weaponToEquip)
    {
        // First, uninstall any weapon we might have
        UnequipWeapon();

        // Instantiate the weapon as a child of the weapon attachment point with the same position and rotation as the weapon attachment point and save it as the player's weapon
        Debug.Log(weaponToEquip + " " + weaponAttachmentPoint);
        weapon = Instantiate(weaponToEquip, weaponAttachmentPoint) as Weapon;

        // Set the weapon's layer to our layer
        weapon.gameObject.layer = this.gameObject.layer;
    }

    // this'll allow is to unequip a weapon
    public void UnequipWeapon()
    {
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        // Set the weapon to null - this should happen automatically, but we can also do it explicitly just to be sure
        weapon = null;
    }

    // Movement
    public abstract void Move(Vector3 direction);

    // the max speed the pawn can rotate
    public float maxRotationSpeed;

    public abstract void RotateToLookAt(Vector3 targetPoint);

    public abstract void Rotate(float speed);
}
