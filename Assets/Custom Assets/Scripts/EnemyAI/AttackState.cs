using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    [Header("Set the distance at which the enemy stops attacking \nthe player.")]
    [SerializeField] float attackRange;

    Transform playerTransform;
    GameObject[] playersArray;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");

        float minDistance = Mathf.Infinity;

        NavMeshAgent navAgent = animator.GetComponent<NavMeshAgent>();

        for (int j = 0; j < playersArray.Length; j++)
        {
            //Get the distance between the player and the enemy
            float currentDistance = Vector3.Distance(playersArray[j].transform.position, animator.transform.position);
            bool canSeePlayer = !navAgent.Raycast(playersArray[j].transform.position, out NavMeshHit hitObstacle);

            if (canSeePlayer && currentDistance < minDistance)
            {
                minDistance = currentDistance;
                playerTransform = playersArray[j].transform;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Rotate enemy to face the player when attacking
        animator.transform.LookAt(playerTransform);
        float distance = Vector3.Distance(playerTransform.position, animator.transform.position);

        //If player moves far enough from enemy, it stops attacking
        if (distance > attackRange)
            animator.SetBool("isAttacking", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
