using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{
    [HideInInspector] public float timer; //timer used for switching between enemy states
    //Transform playerTransform;
    [Header("Set the distance at which the enemy starts chasing \nthe player.")]
    [SerializeField] float chaseRange; //The distance that the enemy detects the player
    [Header("Set wait time when enemy is unalerted.")]
    public float waitTimeWhenPatrolling;
    [Header("Set wait time when enemy is alerted of noise.")]
    public float waitTimeWhenAlerted;
    [Header("Set wait time when enemy arrived at noise location and is investigating.")]
    public float waitTimeWhenInvestigating;

    bool canSeePlayer;
    NavMeshAgent navAgent;
    bool enemyAlerted;
    bool investigatingNoise;
    Vector3 lastNoiseLocation;

    GameObject[] playersArray;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navAgent = animator.GetComponent<NavMeshAgent>();
        investigatingNoise = animator.GetComponent<Enemy>().investigatingNoise;
        //last noise location to compare if it has changed since enemy entered idle state and is investigating, so that it goes back to patrol investigate
        lastNoiseLocation = animator.GetComponent<Enemy>().investigateNoiseLocation; 

        //When idle animation transition starts, reset timer and get player's position using the player
        //tag that i created and assigned to the player object
        timer = 0;

        playersArray = GameObject.FindGameObjectsWithTag("Player");
    }



    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAlerted = animator.GetComponent<Enemy>().enemyAlerted;
        
        if (investigatingNoise) //when enemy arrived at investigation area for noise and look around
        {
            timer += Time.deltaTime;
            if (timer > waitTimeWhenInvestigating)
            {
                animator.GetComponent<Enemy>().enemyAlerted = false;
                animator.GetComponent<Enemy>().investigatingNoise = false;
                animator.SetBool("isPatrolling", true);
            }
            
            //if last noise location changed after enemy arrived at noise location and is investigatin, then start moving again
            if (lastNoiseLocation != animator.GetComponent<Enemy>().investigateNoiseLocation)            
                animator.SetBool("isPatrolling", true);            
        }
        else if (enemyAlerted) //when enemy first alerted of noise
        {
            timer += Time.deltaTime;
            if (timer > waitTimeWhenAlerted)
            {
                //set investigatingNoise to true for use in patrol state when investigating sound and also when enemy enters idle state the 2nd
                //time after he heard a noise
                animator.GetComponent<Enemy>().investigatingNoise = true;
                animator.SetBool("isPatrolling", true);
            }            
        }        
        else
        {
            //Delta time returns the time elapsed between each frame and the next, so it measure the time elapsed, independent
            //of the game's fps. Basically returns real-time elapsed and adds it to the timer variable
            timer += Time.deltaTime;
            if (timer > waitTimeWhenPatrolling) // if 5 seconds pass
            {
                //Set bool parameter i created inside the animator to true for use in controller's state machine. It is used
                //to transition from idle state to walking/partolling state and the other way around
                animator.SetBool("isPatrolling", true);
            }
        }


        //check in all players. No need to get specific player, if any player can be seen the state will change to ChasingState and that script will handle the 
        //selection of the right player to chase.
        foreach (GameObject p in playersArray)
        {
            //Relative point compared to player. If player is in front of enemy, relativePointInFront.z > 0
            var relativePointInFront = animator.transform.InverseTransformPoint(p.transform.position);
            
            //Get distance between player and enemy
            float distance = Vector3.Distance(p.transform.position, animator.transform.position);

            if (distance < chaseRange && relativePointInFront.z > 0) //if enemy within range from the player
            {
                //relativePointInFront is to check if player is in front of enemy and canSeePlayer is to check if
                //there is an object between the player and the enemy, that is obscuring enemy's vision (canSeePlayer alone
                //is true even if player is behind enemy)
                canSeePlayer = !navAgent.Raycast(p.transform.position, out NavMeshHit hitObstacle);
                if (canSeePlayer)
                {
                    //Set bool parameter i created inside the animator to true for use in controller's state machine. It is used
                    //to transition from idle or walking state to run state and from run state to walking state
                    animator.SetBool("isChasing", true);
                    animator.SetBool("doOnce", true); //this animator parameter only used for sound, not used for transitions in state machine
                }

            }

        }
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
