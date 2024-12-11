using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    // the amount of damage that has been dealt
    public float damageDone;

    //the speed of which the weapons fires at
    public float fireRate;

    // the transform of our models right hand
    public Transform RightHandIKTarget;

    // the transform of our models left hand
    public Transform LeftHandIKTarget;

    // headers adds a bold header on a the sting making it more visible to our players
    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
}