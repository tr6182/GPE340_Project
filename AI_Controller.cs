using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// without importing unity ai engine the ai would not work
using UnityEngine.AI;

public class AI_Controller : Controller
{
    [HideInInspector] public NavMeshAgent agent;
    public float stoppingDistance;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // Calls the base class's Start method (optional depending on base class)
    }

    public override void PossessPawn(Pawn pawnToPossess)
    {
        // Possess the pawn
        base.PossessPawn(pawnToPossess);

        // Get the NavMeshAgent off the pawn
        agent = pawn.GetComponent<NavMeshAgent>();

        // If the pawn doesn't have a NavMeshAgent, add one
        if (agent == null)
        {
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();
        }

        // Set the stopping distance
        agent.stoppingDistance = stoppingDistance;

        // Set the max speed of the AI from the pawn data
        agent.speed = pawn.maxMoveSpeed;

        // Set the max rotation speed of the AI from the pawn data
        agent.angularSpeed = pawn.maxRotationSpeed;

        // Disable movement and rotation from the NavMeshAgent
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public override void UnpossessPawn()
    {
        // Remove the NavMeshAgent when unpossessing the pawn
        Destroy(agent);

        // Call base class Unpossess to handle further cleanup
        base.UnpossessPawn();
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Update base logic (if any)
        base.Update();
    }

    protected override void MakeDecisions()
    {
        // If we don't have a pawn or target, we can't make decisions
        if (pawn == null || targetTransform == null)
        {
            return;
        }

        // The NavMeshAgent seeks the target position
        agent.SetDestination(targetTransform.position);

        // Find the velocity that the agent wants to move in order to follow the path
        desiredVelocity = agent.desiredVelocity;

        // Move the pawn using the desired velocity
        pawn.Move(desiredVelocity.normalized);

        // allows the pawn to look at target by rotating position
        pawn.RotateToLookAt(targetTransform.position);
    }
}
