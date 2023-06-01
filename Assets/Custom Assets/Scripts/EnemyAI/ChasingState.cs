using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using static ChasingState;

public class ChasingState : StateMachineBehaviour
{
    [Header("Set how long the enemy will search for the player after he loses him.")]
    [SerializeField] float searchTime;
    [Header("Set the search radius around the player's last known location.")]
    [SerializeField] int searchRadius;

    [Header("Set the enemy speed for when it is chasing the player.")]
    [SerializeField] float enemySpeed;
    [Header("Set the distance at which the enemy stops chasing \nthe player.")]
    [SerializeField] float chaseRange;
    [Header("Set the distance at which the enemy starts attacking \nthe player.")]
    [SerializeField] float attackRange;

    NavMeshAgent navAgent;
    GameObject[] playersArray;
    Transform playerTransform;

    bool canSeePlayer;
    SkinnedMeshRenderer[] playerSkmrs = new SkinnedMeshRenderer[6];    
    Vector3 lastKnownPlayerPosition;
    bool doOnceAfterImage = false;
    static GameObject afterImageGroup; //static to be common for all enemies, so there aren't multiple afterimages for each enemy
    Material afterImageMat;
    [HideInInspector] public float searchTimeTemp;

    private AudioSource audioSource;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");

        navAgent = animator.GetComponent<NavMeshAgent>();
        
        //playerSkmrs = playerTransform.GetChild(2).transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
        //afterImageMat = playerTransform.GetComponent<PlayerStats>().afterImageMaterial;

        navAgent.speed = enemySpeed;

        animator.GetComponent<Enemy>().investigatingNoise = false;

        audioSource = animator.GetComponent<AudioSource>();
        //play sound effect from array when enemy starts chasing
        if (audioSource != null && animator.GetComponent<Enemy>().alertSFX.Count > 0 && animator.GetBool("doOnce"))
        {
            var index = Random.Range(0, animator.GetComponent<Enemy>().alertSFX.Count);
            audioSource.PlayOneShot(animator.GetComponent<Enemy>().alertSFX[index]);
            animator.SetBool("doOnce", false); //this animator parameter only used for sound, not used for transitions in state machine
        }
    }

    
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Sets destination to the player location so that the enemy chases the player
        //navAgent.SetDestination(playerPosition);

        float minDistance = Mathf.Infinity;

        for (int j = 0; j < playersArray.Length; j++)
        {
            //Get the distance between the player and the enemy
            float currentDistance = Vector3.Distance(playersArray[j].transform.position, animator.transform.position);
            canSeePlayer = !navAgent.Raycast(playersArray[j].transform.position, out NavMeshHit hitObstacle);

            if (canSeePlayer && currentDistance < minDistance)
            {
                minDistance = currentDistance;
                playerTransform = playersArray[j].transform;
            }
        }

       

        // I removed canSeePlayer from this if statement because is already checked in the for loop above. The minDistance will only be less than infinity
        // (and thus less than the chaseRange), if a player is found so that has the canSeePlayer bool true else the minDistance will be infinity.
        if (minDistance < chaseRange) //if player is within enemy's visible distance without obstractions
        {
            //Sets destination to the player location so that the enemy chases the player
            navAgent.SetDestination(playerTransform.position);

            lastKnownPlayerPosition = playerTransform.position;
            searchTimeTemp = searchTime;
            doOnceAfterImage = true;
        }
        else if (animator.GetComponent<Enemy>().enemyAlerted) //if enemy can hear player
        {
            //if last known position is the same as before, means that enemy no longer hears the player and thus make enemyAlerted to false.
            //If player makes a noise again enemyAlerted becomes true again from Enemy class
            if (lastKnownPlayerPosition == animator.GetComponent<Enemy>().investigateNoiseLocation)
                animator.GetComponent<Enemy>().enemyAlerted = false;

            lastKnownPlayerPosition = animator.GetComponent<Enemy>().investigateNoiseLocation;

            navAgent.SetDestination(lastKnownPlayerPosition);

            searchTimeTemp = searchTime;
            doOnceAfterImage = true;
        }
        else //else create after image at last known location and go there and search
        {
            playerSkmrs = playerTransform.GetChild(2).transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
            afterImageMat = playerTransform.GetComponent<PlayerStats>().afterImageMaterial;


            #region AfterImage of Player's Last Known Location
            //Leave an AfterImage of the player at last known location when enemy loses sight of him
            if (doOnceAfterImage) //do this only once
            {
                if (afterImageGroup != null)
                    Destroy(afterImageGroup); //destroy previous after image if exists

                afterImageGroup = new GameObject("After Image Group"); //create parent object

                for (int i = 0; i < playerSkmrs.Length; i++)
                {
                    //create gameobject for each modular body part of player and assign them as children
                    Mesh mesh = new Mesh();
                    GameObject afterImage = new GameObject("After Image " + i);
                    afterImage.transform.parent = afterImageGroup.transform;

                    playerSkmrs[i].BakeMesh(mesh, false);

                    afterImage.AddComponent<MeshFilter>();
                    afterImage.AddComponent<MeshRenderer>();

                    afterImage.GetComponent<MeshFilter>().mesh = mesh;

                    //save materials for each modular part. Needed for getting number of materials of each part
                    var tempMaterials = playerSkmrs[i].GetComponent<SkinnedMeshRenderer>().sharedMaterials;

                    for (int j = 0; j < tempMaterials.Length; j++)
                    {
                        tempMaterials[j] = afterImageMat; //assign new trasparent material to all materials of afterimage
                    }

                    afterImage.GetComponent<MeshRenderer>().sharedMaterials = tempMaterials;

                    afterImage.transform.SetPositionAndRotation(playerSkmrs[i].transform.position, playerSkmrs[i].transform.rotation);
                }
                doOnceAfterImage = false;

                //Go to last known location where after image is
                navAgent.SetDestination(lastKnownPlayerPosition);
            }
        }

        #endregion


        #region Search Player At Random Location Within A Radius

        searchTimeTemp -= Time.deltaTime;

        if (navAgent.remainingDistance <= 2)
        {
            if (afterImageGroup != null)
                Destroy(afterImageGroup);

            if (searchTimeTemp > 0)
            {
                //get random point inside shpere with radius 1 and then multiplied to increase radius
                Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
                randomDirection += animator.transform.position; //add random spot to enemy transform so that it the search radius move with enemy

                //find nearest point from randomDirection, inside navmesh
                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, searchRadius, 1);
                Vector3 randomDestination = hit.position;
                navAgent.SetDestination(randomDestination);
            }
            else
            {
                animator.SetBool("isChasing", false);
            }
        }
        #endregion



        //When enemy moves close enough to the player it starts attacking and enters the attack state
        if (minDistance < attackRange)
            animator.SetBool("isAttacking", true);
    }

    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Sets the destination the location that the enemy is currently at, in order for it to stop moving,
        //when the player move far enough from the enemy (chaseRange) and the state exits the run state
        navAgent.SetDestination(animator.transform.position);
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
