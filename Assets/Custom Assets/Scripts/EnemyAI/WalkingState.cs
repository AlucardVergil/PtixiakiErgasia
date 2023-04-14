using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingState : StateMachineBehaviour
{
    float timer; //timer used for switching between enemy states
    float timeToStopMoving; //the time that the enemy stops moving
    bool canSeePlayer;
    bool investigatingNoise;

    //List of all the waypoints transforms that the enemy patrols.
    //Waypoints are empty object created and placed in certain locations inside the scene
    List<Transform> waypoints = new List<Transform>();
    //NavMeshAgent object that is used to map the scene so that the enemy knows where and how he can navigate inside the scene
    NavMeshAgent navAgent; 

    Transform playerPosition;
    [Header("Set the enemy speed for when it is NOT chasing the player.")]
    [SerializeField] float enemySpeed;
    [Header("Set the distance at which the enemy starts chasing \nthe player.")]
    [SerializeField] float chaseRange;

    Vector3 investigateLocation;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        navAgent = animator.GetComponent<NavMeshAgent>(); //Get navigation mesh agent of the enemy
        investigatingNoise = animator.GetComponent<Enemy>().investigatingNoise;

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform; //Get player position using the tag "Player"
        navAgent.speed = enemySpeed; //Set the enemy's speed from inside the navmeshagent component of the enemy

        if (!investigatingNoise) //don't execute this code if enemy is investigating noise
        {
            timer = 0;
            timeToStopMoving = Random.Range(5, 10); //the enemy moves for 5-10 seconds

            //Get the parent "waypoints" object from the scene, which contains all the waypoints as children and has
            //the "WaypointsTag" tag that i created and assigned to it
            GameObject waypointsObject = GameObject.FindGameObjectWithTag("WaypointsTag");

            //Get every waypoint's transform and add it to the waypoints list
            foreach (Transform t in waypointsObject.transform)
                waypoints.Add(t);

            //Set a random destination among all the waypoints, for the enemy to move to
            navAgent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
        }        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (investigatingNoise)
        {
            investigateLocation = animator.GetComponent<Enemy>().investigateNoiseLocation; //update location each time enemy hears a sound

            if (navAgent.destination != investigateLocation) //if destination changed from last noise location
                navAgent.SetDestination(investigateLocation);

            if (navAgent.remainingDistance <= navAgent.stoppingDistance) //stop when arrived at location
                animator.SetBool("isPatrolling", false);        
        }
        else 
        {
            //remainingDistance is the distance from the enemy's current position to the destination's position.
            //stoppingDistance is the distance between the enemy and the destination, that the enemy has to stop moving.
            //So if the remainingDistance is less or equal to the stoppingDistance, it means that the enemy is already
            //at that waypoint and so this waypoint is invalid and it has to choose a different one.
            //For example if the stopping distance is 5 meters from the destination and the remaining distance to the
            //destination is less that 5, it means that the enemy is already 5 meters within the destination point.
            //NOTE: this could mean that either it chose the same waypoint twice in a row or that the next waypoint is 
            //too close to the previous one or that the enemy reached the destination in less than 10 secs (which is the time
            //limit that i set below for the enemy to move) and so it chose another point instantly without switching to the
            //idle state first. So the enemy doesn't go to idle when he reaches its destination but instead when the set time 
            //passes and so it could travel to more than one waypoint within the time limit. So the enemy could go to idle at
            //any location between the waypoints and not just at the exact location the waypoints are placed
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
                navAgent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);

            timer += Time.deltaTime;
            if (timer > timeToStopMoving) //Stop patrolling after 5-10 secs
                animator.SetBool("isPatrolling", false);
        }
        

        //Relative point compared to player. If player is in front of enemy, relativePointInFront.z > 0
        var relativePointInFront = animator.transform.InverseTransformPoint(playerPosition.position);
            
        float distance = Vector3.Distance(playerPosition.position, animator.transform.position);
        if (distance < chaseRange && relativePointInFront.z > 0)
        {
            //relativePointInFront is to check if player is in front of enemy and canSeePlayer is to check if
            //there is an object between the player and the enemy, that is obscuring enemy's vision (canSeePlayer alone
            //is true even if player is behind enemy)
            canSeePlayer = !navAgent.Raycast(playerPosition.position, out NavMeshHit hitObstacle);
            if (canSeePlayer)
            {
                animator.SetBool("isChasing", true);
                animator.SetBool("doOnce", true); //this animator parameter only used for sound, not used for transitions in state machine
            }                                      
        }
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Sets the destination the location that the enemy is currently at, in order for it to stop moving,
        //when the timer hits the time limit (10 secs) and the state exits the walk state
        navAgent.SetDestination(navAgent.transform.position);
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
