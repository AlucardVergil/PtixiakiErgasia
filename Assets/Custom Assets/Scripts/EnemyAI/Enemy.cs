using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

//Could add below 2 lines but i will need to also set the settings for these components
//[RequireComponent(typeof(BoxCollider))]
//[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : NetworkBehaviour
{
    private NetworkVariable<float> networkHP = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public int hp; //Health of enemy
    public Animator animator; //Enemy animator component
    public Slider HPbar; //Enemy health bar slider above enemy's head
    private float slowMoTimer = 0;
    private bool canExecuteSlowMo = true; //bool to stop infinitely executing slowMo effect when enemy is dead because update keeps executing even after death

    public bool isAttackingPlayer = false;
    GameObject enemySpawn;

    [Header("Enter noise threshold that the enemy can hear the player.")]
    public float noiseThreshold;

    //true when enemy first hears noise and false after he arrived at investigation area and waited there and went back to normal behaviour
    [HideInInspector] public bool enemyAlerted;
    //true when enemy starts moving after he heard the noise and false after he arrived at investigation area and waited there and went back to normal behaviour
    [HideInInspector] public bool investigatingNoise;
    [HideInInspector] public Vector3 investigateNoiseLocation;
    private GameObject player;
    //private PlayerStats playerStats;
    private PlayerNoiseLevels playerNoise;
    private AudioSource audioSource;
    public List<AudioClip> alertSFX;
    public Image alertImage;
    private float timer;

    GameObject[] playersArray;
    float maxNoiseMade;


    //Function called when player's weapon hits the enemy in order for the enemy to take damage
    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc(int damageValue, float critDamage, int critChance)
    {        

        TakeDamageClientRpc(damageValue, critDamage, critChance);


        //Spawn enemy at spawn position
        //GameObject Enemy = Instantiate(Resources.Load("EnemySkeleton (4)", typeof(GameObject))) as GameObject;

        //Enemy.transform.position = enemySpawn.transform.position;

        //Enemy.GetComponent<NetworkObject>().Spawn(true);
    }


    [ClientRpc]
    public void TakeDamageClientRpc(int damageValue, float critDamage, int critChance)
    {
        Debug.Log("OwnerClientId " + OwnerClientId + " LocalPlayer " + NetworkManager.Singleton.LocalClientId + " networkHP " + networkHP.Value);


        //get random between 0-100 and if it is smaller than the crit chance of the player, multiply damage value with crit damage
        var rnd = Random.Range(0, 100);
        damageValue = (int)Mathf.Round(rnd > critChance ? damageValue : damageValue * critDamage);


        networkHP.Value -= damageValue;//Decrease enemy health based on the damage dealt by the player

        
        if (networkHP.Value <= 0)
        {
            //set the trigger parameter in state machine so that the enemy will transition to the death animation
            //animator.SetTrigger("die");
            //Disable enemy's collider so that take damage function won't trigger again if player hits a dead enemy
            GetComponent<Collider>().enabled = false;
            HPbar.gameObject.SetActive(false);

            StartCoroutine(GetComponent<DissolvingController>().Dissolve());            
        }
        else
        {
            //set the trigger parameter in state machine so that the enemy will transition to the takeDamage animation when hit.
            //Also no need to add transition condition from TakeDamage to Run. It is done automatically when you hit the enemy
            //with just an empty transition, so that the enemy starts chasing the player when he takes damage
            animator.SetBool("isChasing", true);
            animator.SetTrigger("damage");
        }
    }



    void Start()
    {
        networkHP = new NetworkVariable<float>(hp, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        HPbar.maxValue = hp; //Automatically change the max value of the slider to match HP of the enemy
        enemySpawn = GameObject.Find("EnemySpawn"); //Find empty gameobject to spawn enemy

        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        // Assign the correct player gameobject for each player by checking if they are owner of the player gameobject
        foreach (GameObject p in playersArray)
        {
            if (p.GetComponent<NetworkObject>().IsLocalPlayer)
                player = p;
        }

        playerNoise = player.GetComponent<PlayerNoiseLevels>();

        audioSource = GetComponent<AudioSource>();


        HPbar.value = networkHP.Value;

        networkHP.OnValueChanged += (float previousValue, float newValue) =>
        {
            Debug.Log("OwnerClientId " + OwnerClientId + " LocalPlayer " + NetworkManager.Singleton.LocalClientId + " old " + previousValue);
            Debug.Log("OwnerClientId " + OwnerClientId + " LocalPlayer " + NetworkManager.Singleton.LocalClientId + " new " + newValue);

            HPbar.value = newValue; //Change the HP slider based on the remaining HP of the enemy

            //Slow Motion Effect When Enemy Dies
            //if (newValue <= 0 && canExecuteSlowMo)
            //{
            //    slowMoTimer += Time.deltaTime;

            //    if (slowMoTimer < 2 * Time.timeScale) // DeltaTime is affected by timescale so i multiply it with the current timescale so that i get the real-time
            //        Time.timeScale = 0.5f;
            //    else
            //    {
            //        Time.timeScale = 1f;
            //        slowMoTimer = 0;
            //        canExecuteSlowMo = false;
            //    }
            //}
            
        };
    }


    //void Update()
    //{
    //    HPbar.value = networkHP.Value; //Change the HP slider based on the remaining HP of the enemy

    //    //Slow Motion Effect When Enemy Dies
    //    if (networkHP.Value <= 0 && canExecuteSlowMo)
    //    {
    //        slowMoTimer += Time.deltaTime;

    //        if (slowMoTimer < 2 * Time.timeScale) // DeltaTime is affected by timescale so i multiply it with the current timescale so that i get the real-time
    //            Time.timeScale = 0.5f;
    //        else
    //        {
    //            Time.timeScale = 1f;
    //            slowMoTimer = 0;
    //            canExecuteSlowMo = false;                
    //        }
    //    }

    //    PlayerNoiseMade();

    //    if (!alertImage.gameObject.activeSelf)
    //        timer = 0;

    //    timer += Time.deltaTime;

    //    if (timer > 3)
    //        alertImage.gameObject.SetActive(false);
    //}


    private void Update()
    {
        PlayerNoiseMade();

        if (!alertImage.gameObject.activeSelf)
            timer = 0;

        timer += Time.deltaTime;

        if (timer > 3)
            alertImage.gameObject.SetActive(false);
    }



    void PlayerNoiseMade()
    {
        playersArray = GameObject.FindGameObjectsWithTag("Player");

        maxNoiseMade = 0;

        // Assign the correct player gameobject for each player by checking if they are owner of the player gameobject
        foreach (GameObject p in playersArray)
        {
            playerNoise = p.GetComponent<PlayerNoiseLevels>();

            //Player noise level based on player action perform divided by distance to the enemy
            float distance = Vector3.Distance(p.transform.position, animator.transform.position);
            float noiseMade = playerNoise.noiseLevel / distance;

            if (noiseMade > maxNoiseMade)
                maxNoiseMade = noiseMade;
        }        


        if (maxNoiseMade > noiseThreshold)
        {
            investigateNoiseLocation = player.transform.position;

            //if enemy is already alerted go straight to walk not idle. Idle is only for first time enemy hears noise
            //Also if enemy is already alerted and the starts chasing, this doesn't execute since enemyAlerted is already true
            //If enemy wasn't already alerted and starts chasing, then it executes and sets enemyAlerted to true and isPatrolling to false, so
            //when he loses you he goes first to idle state and waits a bit and then resumes normal behaviour
            if (!investigatingNoise && !enemyAlerted) 
            {
                animator.SetBool("isPatrolling", false);                
                animator.GetBehaviour<IdleState>().timer = 0; //if already in idle state and waiting reset timer
                enemyAlerted = true;

                if (!animator.GetBool("isChasing"))
                    alertImage.gameObject.SetActive(true);
            }
        }

    }   
    


    public void StartAttacking()
    {
        isAttackingPlayer = true;
    }


    public void EndAttacking()
    {
        isAttackingPlayer = false;
    }
    
}
