using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Globalization;
using Unity.Netcode;


public class SpawnWallSpell : NetworkBehaviour
{
    [Header("SPAWN WALL SPELL")]
    public GameObject[] wallSpellArray;
    public GameObject wallEffect;
    public GameObject wallWarmUp;
    public float wallWarmUpDelay = 1;
    public Transform wallWarmUpPoint_Left;
    public Transform wallWarmUpPoint_Right;
    public float spellCooldown;
    public float pillarsDuration = 30f;
    public List<AudioClip> wallSFX;
    public GameObject raycast;
    public int numberOfPillars = 8;
    public float spaceBetweenPillars = 0.8f;
    public float speed = 10f;
    public GameObject spellIcon;

    private GameObject wallSpell;
    private float timeToFire = 0;
    private AudioSource audioSource;
    private Animator anim;

    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float[] journeyLength;
    private GameObject[] obj;
    private Vector3[] start;
    private Vector3[] end;


    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        journeyLength = new float[numberOfPillars];
        obj = new GameObject[numberOfPillars];
        start = new Vector3[numberOfPillars];
        end = new Vector3[numberOfPillars];

        //feed the cooldown icon class the spell cooldown and time to fire
        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);
    }


    private void Update()
    {
        //loop through all individual pillars and use lerp to move them from underground to the surface
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null && obj[i].transform.position != end[i])
            {
                float distanceCovered = (Time.time - startTime) * speed;
                float percentageOfJourneyCovered = distanceCovered / journeyLength[i];

                obj[i].transform.position = Vector3.Lerp(start[i], end[i], percentageOfJourneyCovered);
            }
        }
    }


    // Update is called once per frame
    public void ExecuteWallSpell()
    {
        if (!IsOwner) return; // For NetworkBehaviour

        if (Time.time >= timeToFire)
        {
            //save raycast position before triggering animation because when animation plays it moves the raycast position
            var raycastPosition = raycast.transform.position;
            anim.SetTrigger("spawnWall");
            timeToFire = Time.time + spellCooldown; //sets cooldown of spell

            if (wallSpellArray != null)
            {
                StartCoroutine(InstantiateSpawnWallSpell(raycastPosition));
            }
        }
        //feed the cooldown icon class the spell cooldown and time to fire
        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);
    }


    public void ExecuteKickWall()
    {
        var pillarsGroup = GameObject.FindGameObjectsWithTag("PillarsGroup");

        //Kick wall to break it
        foreach (var group in pillarsGroup)
        {
            for (int i = 0; i < group.transform.childCount; i++)
            {
                //get direction of where the player is looking and if he is looking at the wall and is close enough kick it 
                Vector3 direction = (group.transform.GetChild(i).transform.position - transform.position).normalized;
                float dot = Vector3.Dot(direction, transform.forward);

                var distance = Vector3.Distance(transform.position, group.transform.GetChild(i).transform.position);

                if (distance < 2.5 && dot > 0.8)
                {
                    for (int j = 0; j < group.transform.childCount; j++)
                    {
                        group.transform.GetChild(j).GetComponent<Fracture_Pillar>().SetDestroyWallBool(true);
                    }

                    Destroy(group, 5);
                }
            }
        }

    }



    IEnumerator InstantiateSpawnWallSpell(Vector3 raycastPosition)
    {
        GetComponent<PlayerInput>().enabled = false;

        //create the spell warmup in hand and parent it to hand to move along with it and destroy
        //the moment the actual spell is created and fired
        if (wallWarmUp != null)
        {
            var warmUpObjLeft = Instantiate(wallWarmUp, wallWarmUpPoint_Left.position, Quaternion.identity);
            warmUpObjLeft.transform.parent = wallWarmUpPoint_Left.transform;
            Destroy(warmUpObjLeft, wallWarmUpDelay);

            var warmUpObjRight = Instantiate(wallWarmUp, wallWarmUpPoint_Right.position, Quaternion.identity);
            warmUpObjRight.transform.parent = wallWarmUpPoint_Right.transform;
            Destroy(warmUpObjRight, wallWarmUpDelay);
        }

        if (audioSource != null && wallSFX.Count > 0)
        {
            var index = Random.Range(0, wallSFX.Count);
            audioSource.PlayOneShot(wallSFX[index]);
        }       

        yield return new WaitForSeconds(wallWarmUpDelay);        


        if (!GetComponent<PlayerStats>().dead)
        {
            //Create empty gameobject named "PillarsGroup" in order to group pillars together under it as parent and then add tag to parent
            GameObject parentObj = new GameObject("PillarsGroup");
            parentObj.tag = "PillarsGroup";

            Vector3 newRaycast = new(0, 0, 0);
            var temp = 23;
            for (int i = 0; i < obj.Length; i++)
            {
                wallSpell = wallSpellArray[Random.Range(0, wallSpellArray.Length)];

                //var temp = Random.Range(30, 40);


                if (i < obj.Length / 2)
                    temp += 4;
                else
                    temp -= 4;

                wallSpell.transform.localScale = new Vector3(temp, temp, temp);

                //var objectSize = wallSpell.GetComponent<NavMeshObstacle>().size;
                var rend = wallSpell.GetComponent<Renderer>(); //get pillar renderer to get pillar dimensions

                var bounds_Y = rend.bounds.max.y - rend.bounds.min.y; //get pillar height.

                //Multiply by Euler to rotate the first few pillars -45 degrees
                var diagonal = Quaternion.Euler(0f, -45f, 0f) * transform.right;
                var bounds_X_1 = (rend.bounds.max.x - rend.bounds.min.x) * diagonal.x * spaceBetweenPillars;
                var bounds_Z_1 = (rend.bounds.max.x - rend.bounds.min.x) * diagonal.z * spaceBetweenPillars;

                //get mesh bounds from one corner to the other (basically the size) and multiply by tranform.right to rotate based
                //on character rotation. Bounds_Z has bounds.x instead of z because the pillar rotates and i only want the x size
                //but i still multiply it with right.z to get the correct orientation.
                var bounds_X_2 = (rend.bounds.max.x - rend.bounds.min.x) * transform.right.x * spaceBetweenPillars;
                var bounds_Z_2 = (rend.bounds.max.x - rend.bounds.min.x) * transform.right.z * spaceBetweenPillars;

                //Multiply by Euler to rotate the last few pillars 45 degrees
                var diagonal2 = Quaternion.Euler(0f, 45f, 0f) * transform.right;
                var bounds_X_3 = (rend.bounds.max.x - rend.bounds.min.x) * diagonal2.x * spaceBetweenPillars;
                var bounds_Z_3 = (rend.bounds.max.x - rend.bounds.min.x) * diagonal2.z * spaceBetweenPillars;

                //get new raycast position by adding vector3 with the size of the mesh and multiplying by i so that in each loop it 
                //moves the raycast in the next position 
                //var newRaycast = raycastPosition + new Vector3(bounds_X * i, 0, bounds_Z * i);

                //get new raycast position by adding vector3 with the size of the mesh in relation to the last position
                //so that in each loop it moves the raycast in the next position 
                if (i < 3)
                {
                    if (i == 0)
                        newRaycast = raycastPosition;

                    newRaycast += new Vector3(bounds_X_1, 0, bounds_Z_1);
                }
                else if (i < 6)
                {
                    newRaycast += new Vector3(bounds_X_2, 0, bounds_Z_2);
                }
                else
                    newRaycast += new Vector3(bounds_X_3, 0, bounds_Z_3);


                //cast a ray from raycast empty gameobject's position, towards the ground and get hit point
                Physics.Raycast(newRaycast, transform.TransformDirection(Vector3.down), out RaycastHit hit);

                //Debug.DrawRay(newRaycast, transform.TransformDirection(Vector3.down) * hit.distance, Color.red, 30f);

                //get underground position by dividing the Y with the pillar's height
                Vector3 positionUnderground = new(hit.point.x, hit.point.y - bounds_Y, hit.point.z);

                //Debug.Log(wallSpell.transform.rotation.eulerAngles.z);
                //set stone pillar rotation based on player's y rotation
                //var test2 = -90 * transform.right.x;
                //var test = Random.Range(-70, -110) * transform.right.x;
                //var pillarRotation = Quaternion.Euler(test, transform.rotation.eulerAngles.y, test2);
                //Debug.Log(pillarRotation.eulerAngles);

                //Debug.Log("Right " + transform.right);
                //Debug.Log("Forward " + transform.forward);
                //var test = Random.Range(-70, -110);
                //var test2 = new Vector3(test, 0, 0);
                //wallSpell.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.right);
                //var test2 = Quaternion.Euler(0, 0, test) * transform.right;
                //var pillarRotation = Quaternion.Euler(test3.x, transform.rotation.eulerAngles.y, wallSpell.transform.rotation.eulerAngles.z);
                //var pillarRotation = Quaternion.Euler(wallSpell.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, test2.z);

                //var rnd = Random.Range(-10, 10);
                //var pillarRotation = Quaternion.Euler(wallSpell.transform.rotation.eulerAngles.x + rnd, transform.rotation.eulerAngles.y, wallSpell.transform.rotation.eulerAngles.z + rnd);

                //get rotation of pillar except from Y which i get from player character in order to rotate pillar based on character rotation
                var pillarRotation = Quaternion.Euler(wallSpell.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, wallSpell.transform.rotation.eulerAngles.z);
                obj[i] = Instantiate(wallSpell, positionUnderground, pillarRotation); //create spell in position in front of player

                //group all pillars created with each use of the spell under one empty gameobject
                obj[i].transform.parent = parentObj.transform;

                //set start and end position for each pillar (underground and then on surface)
                start[i] = obj[i].transform.position;
                end[i] = new Vector3(obj[i].transform.position.x, obj[i].transform.position.y + bounds_Y, obj[i].transform.position.z);

                //get start time for pillars to move
                startTime = Time.time;
                //get distance for pillars to travel
                journeyLength[i] = Vector3.Distance(start[i], end[i]);

                //destroy pillars after 30 secs
                Destroy(obj[i], pillarsDuration);
            }

            yield return new WaitForSeconds(1); //wait 1 sec after firing spell to finish casting animation
            GetComponent<PlayerInput>().enabled = true; //re-enable player inputs after firing spell
        }

        


        /*
        while(obj[0].transform.position != end[0])
        {
            //loop through all individual pillars and use lerp to move them from underground to the surface
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] != null)
                {
                    float distanceCovered = (Time.time - startTime) * speed;
                    float percentageOfJourneyCovered = distanceCovered / journeyLength[i];

                    obj[i].transform.position = Vector3.Lerp(start[i], end[i], percentageOfJourneyCovered);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        */
    }

    public void Testingg()
    {
        GetComponent<PlayerInput>().enabled = true;
    }
}