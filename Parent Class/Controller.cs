
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the Controller class is a parent class (child class can inherits aspect from the parent class)
public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;

    protected virtual void Start()
    {
        // If we were given a pawn at start, possess it
        if (pawn != null)
        {
            PossessPawn(pawn);
        }
    }

    protected virtual void Update()
    {
        // Make decisions
        MakeDecisions();
    }

    protected abstract void MakeDecisions();

    public virtual void PossessPawn(Pawn pawnToPossess)
    {
        // Set our pawn variable to the pawn we want to possess
        pawn = pawnToPossess;

        // Set the pawn's controller to this controller
        pawn.controller = this;

        // Set the pawn's layer to this controller's layer
        pawn.gameObject.layer = this.gameObject.layer;
    }

    public virtual void UnpossessPawn()
    {
        // Set the pawn's controller to this null
        pawn.controller = null;

        // Set our pawn variable to null
        pawn = null;
    }
}
