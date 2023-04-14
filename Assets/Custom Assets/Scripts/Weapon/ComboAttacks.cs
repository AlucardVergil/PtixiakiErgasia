using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ComboAttacks : MonoBehaviour
{
    public static int numOfClicks = 0;
    public string animationName;
    public string animationWeaponPrefix; //What type of weapon the player uses, in order to play the corresponding animation
    
    private float nextFireTime = 0.7f;
    private Animator anim;
    private float lastClickedTime = 0;    
    private float minComboDelay = 0.5f;

    private float maxComboDelay = 1;
    public bool canTransitionAttack = true;

    private StarterAssetsInputs _input;  
    private float mouseHoldTime = 0;

    private int staminaDrainValue;
    [HideInInspector] public float weaponWeight;

    bool sprintAttackUnlocked;
    bool roundAttackUnlocked;
    bool dashUnlocked;
    int dashDistance = 1;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        //Super speed dash forward 
        //if (Input.GetKeyDown(KeyCode.R) && dashUnlocked)
            //DashSuperSpeedMove();


        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.A))
                anim.CrossFadeInFixedTime("Standing Dodge Left", 0.25f);
            if (Input.GetKeyDown(KeyCode.S))
                anim.CrossFadeInFixedTime("Standing Dodge Backward", 0.25f);
            if (Input.GetKeyDown(KeyCode.D))
                anim.CrossFadeInFixedTime("Standing Dodge Right", 0.25f);
        }


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numOfClicks = 0;
            StopAllCoroutines();
        }
        /*
        if (Input.GetMouseButton(0) && roundAttackUnlocked)
        {
            mouseHoldTime += Time.deltaTime;
            if (mouseHoldTime > 0.5f)
                animationName = "Round Attack";
            staminaDrainValue = 30;
        }        
        else
        {
            mouseHoldTime = 0;
            animationName = "Light Attack";
            staminaDrainValue = numOfClicks >= 3 ? 30 : 20;
        }

        if (_input.sprint && Input.GetMouseButtonUp(0) && _input.move != Vector2.zero && sprintAttackUnlocked)
        {
            animationName = "Sprint Attack";
            staminaDrainValue = 30;
        }


        if (Time.time > nextFireTime && (Input.GetMouseButtonUp(0) || mouseHoldTime > 0.5f))
        {
            if (_input.attack && !GetComponent<PlayerStats>().dead && staminaDrainValue < GetComponent<PlayerStats>().stamina) 
                StartCoroutine(ComboAttackOnClick(animationWeaponPrefix + " " + animationName, staminaDrainValue));

            _input.attack = false;
        }

        if (Input.GetMouseButtonUp(0))
            _input.attack = false;
        */
    }


    public void ExecuteWeaponAttack(InputAction.CallbackContext context)
    {
        if (!GetComponent<SheathWeapon>().sheathBool && !Input.GetMouseButton(1))
        {
            if (context.interaction is HoldInteraction && roundAttackUnlocked)
            {
                animationName = "Round Attack";
                staminaDrainValue = 30;
            }

            if (_input.sprint && context.interaction is TapInteraction && _input.move != Vector2.zero && sprintAttackUnlocked)
            {
                animationName = "Sprint Attack";
                staminaDrainValue = 30;
            }
            else if (context.interaction is TapInteraction)
            {
                mouseHoldTime = 0;
                animationName = "Light Attack";
                staminaDrainValue = numOfClicks >= 3 ? 30 : 20;
            }


            if (Time.time > nextFireTime)
            {
                if (!GetComponent<PlayerStats>().dead && staminaDrainValue < GetComponent<PlayerStats>().stamina)
                    StartCoroutine(ComboAttackOnClick(animationWeaponPrefix + " " + animationName, staminaDrainValue));
            }
        }            
    }



    IEnumerator ComboAttackOnClick(string animation, float staminaDrain)
    {
        //I set it as a big number (infinity) just so it doesn't trigger the if statement in update,
        //until the animation event sets it to the right value for current animation
        maxComboDelay = Mathf.Infinity;
        
        lastClickedTime = Time.time;
        nextFireTime = Time.time + minComboDelay;

        //wait until canTransitionAttack becomes true from inside animation event at the end of the attack animation
        yield return new WaitUntil(() => canTransitionAttack);

        //drain stamina based on type of move and weapon weight. I used staminaDrain parameter instead of the staminaDrainValue var, so the value
        //won't accidentally change if you click multiple time, while the function is suspended in yield waitUntil()
        GetComponent<PlayerStats>().DrainStamina(staminaDrain + (weaponWeight * 5)); 

        numOfClicks++;

        animation += " " + numOfClicks;
        var stateID = Animator.StringToHash(animation);        

        if (anim.HasState(0, stateID) && !GetComponent<PlayerStats>().dead)
        {
            anim.CrossFadeInFixedTime(animation, 0.25f);
            canTransitionAttack = false;
        }
        else
        {
            numOfClicks = 0;
            StopAllCoroutines();
        }            
    }


    public void SetMaxComboDelayAndCanTransitionAttack(AnimationEvent animationEvent)
    {
        canTransitionAttack = true;
        maxComboDelay = animationEvent.floatParameter;
    }


    //Function for dashing forward
    public void DashSuperSpeedMove()
    {
        if (dashUnlocked)
            GetComponent<CharacterController>().Move(transform.forward * dashDistance);
    }


    public void UnlockSprintAttack()
    {
        sprintAttackUnlocked = true;
    }


    public void UnlockRoundAttack()
    {
        roundAttackUnlocked = true;
    }  
    

    public void UnlockDash(int distance)
    {
        if (dashUnlocked)
            dashDistance += distance;

        dashUnlocked = true;        
    }
}