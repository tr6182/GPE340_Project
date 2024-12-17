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

    public float maxAccuracyRotation;

    [HideInInspector] public Pawn owner;

    public virtual float GetAccuracyRotationDegrees()
    {
        // Return a random percentage between min and max AccuracyRoation 
        // Get a random number between 0 and 1 ( a percentage )
        float accuracyDeltaPercentage = Random.value;

        // Find that percentage between the negative (to the Left) and positive (to the right) values of this rotation.
        float accuracyDeltaDegrees = Mathf.Lerp(-maxAccuracyRotation, maxAccuracyRotation, accuracyDeltaPercentage);

        // Return that value
        return accuracyDeltaDegrees;
    }

    // headers adds a bold header on a the sting making it more visible to our players
    [Header("Events")]
    public UnityEvent OnPrimaryAttackBegin;
    public UnityEvent OnPrimaryAttackEnd;
    public UnityEvent OnSecondaryAttackBegin;
    public UnityEvent OnSecondaryAttackEnd;
}