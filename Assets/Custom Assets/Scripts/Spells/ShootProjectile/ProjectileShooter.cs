using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using StarterAssets;
using Unity.Netcode;


public class ProjectileShooter : NetworkBehaviour
{
    //Normal & Aim Camera
    public GameObject playerFollowCamera;
    public GameObject playerAimCamera;
    [SerializeField] GameObject crosshair;

    [Space]
    [Header("SHOOT SPELL PROJECTILE")]
    public Camera cam;
    public GameObject projectile;
    public GameObject warmUp;
    public float warmUpDelay = 1;
    public float projectileSpeed = 10;
    public Transform firePoint;
    public float spellCooldown;       
    public List<AudioClip> shootSFX;

    public GameObject spellIcon;

    private StarterAssetsInputs _input;

    [Space]
    [Header("SHAKE OPTIONS & PP")]
    public Volume volume;
    public float chromaticGoal = 0.5f;
    public float chromaticRate = 0.1f;
    public CinemachineImpulseSource impulseSource;
    public float shakeDuration = 1;
    public float shakeAmplitude = 5;
    public float shakeFrequency = 2.5f;

    private float timeToFire = 0;
    private ChromaticAberration chromatic;
    private AudioSource audioSource;
    private Animator anim;
    private Vector3 destination;
    private bool chromaticIncrease;



    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //volume.profile.TryGet<ChromaticAberration>(out chromatic);
        anim = GetComponent<Animator>();
        //crosshair = GameObject.FindGameObjectWithTag("CrossHair");

        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return; // For NetworkBehaviour

        //Change to aim camera if right mouse click is held down
        if (Input.GetMouseButton(1) && _input.playerControls.Player.enabled)
        {
            playerFollowCamera.SetActive(false);
            playerAimCamera.SetActive(true);
            crosshair.SetActive(true);
            
            if (Input.GetMouseButtonDown(0) && Time.time >= timeToFire)//if (Input.GetKeyDown("Fire1") && Time.time >= timeToFire)
            {
                anim.SetTrigger("fire");
                timeToFire = Time.time + spellCooldown; //sets cooldown of spell

                if (projectile != null)
                {
                    ShootProjectile();
                }
            }
        }
        else
        {
            playerFollowCamera.SetActive(true);
            playerAimCamera.SetActive(false);
            crosshair.SetActive(false);
        }
        //feed cooldown icon class the spell cooldown and time to fire
        spellIcon.GetComponent<CooldownTimer>().SetCooldownAndTimeToFire(spellCooldown, timeToFire);
    }


    void ShootProjectile()
    {        
        Ray ray = cam.ViewportPointToRay(new Vector3(0.51f, 0.5f, 0)); //return ray from middle of viewport        
        
        //if ray hits a collider set destination to the hit point else set destination to 1000 units along the ray
        if (Physics.Raycast(ray, out RaycastHit hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        gameObject.transform.forward = ray.GetPoint(1000); //rotate player to face direction of target where he throws the spell
        
        GetComponent<PlayerInput>().enabled = false; //disable player inputs when firing spell        

        StartCoroutine(InstantiateProjectile(firePoint)); //instantiate spell on left hand of player
    }


    IEnumerator InstantiateProjectile(Transform firepoint)
    {
        //create the spell warmup in hand and parent it to hand to move along with it and destroy
        //the moment the actual spell is created and fired
        if (warmUp != null)
        {
            var warmUpObj = Instantiate(warmUp, firepoint.position, Quaternion.identity);
            warmUpObj.transform.parent = firepoint.transform;
            Destroy(warmUpObj, warmUpDelay);
        }
        //play sound effect from array
        if (audioSource != null && shootSFX.Count > 0)
        {
            var index = Random.Range(0, shootSFX.Count);
            audioSource.PlayOneShot(shootSFX[index]);
        }
        //suspend execution of function for given secs
        yield return new WaitForSeconds(warmUpDelay);

        if (!GetComponent<PlayerStats>().dead)
        {
            //ShakeCameraWithImpulse();
            //StartCoroutine(ChromaticAberrationPunch());

            var projectileObj = Instantiate(projectile, firepoint.position, Quaternion.identity); //create spell in hand
            var distance = destination - firepoint.position; //get distance between hand and destination of ray
                                                             //set the speed of spell based on the variable and the distance normalized
            projectileObj.GetComponent<Rigidbody>().velocity = distance.normalized * projectileSpeed;

            yield return new WaitForSeconds(1); //wait 1 sec after firing spell to finish casting animation

            GetComponent<PlayerInput>().enabled = true; //re-enable player inputs after firing spell
        } 
    }
}