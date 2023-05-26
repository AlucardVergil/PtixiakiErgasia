using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class PlayerStats : NetworkBehaviour
{
    [SerializeField] int hp; //Health of player    
    Animator animator; //Player animator component
    public Slider PlayerHPbar; //Player health bar
    public Slider PlayerStaminabar; //Player stamina bar
    private float slowMoTimer = 0;
    private bool canExecuteSlowMo = true;

    public Gradient HPgradient;
    GameObject deathMenu;
    GameObject pauseMenu;

    public TMP_Text hpText;
    private float hpPercentage;
    [HideInInspector] public bool dead;

    GameObject helpPanel;

    public float stamina = 300; //Stamina of player    
    public float refillStaminaDelay = 2;
    public int staminaRefillRate = 5;
    private float lastDrainStaminaTime;

    [Header("Enter player crit chance from 0 - 100%")]
    public int critChance;
    [Header("Enter player crit damage between 1.1 (meaning 10% increase) to whatever.")]
    public float critDamage;

    [Header("Set AfterImage material for player, for use in enemies state machine")]
    public Material afterImageMaterial;



    void Start()
    {
        animator = GetComponent<Animator>();

        PlayerHPbar.maxValue = hp; //Automatically change the max value of the slider to match HP of the player
        GameObject.FindGameObjectWithTag("PlayerSliderFill").GetComponent<Image>().color = HPgradient.Evaluate(1f);
        deathMenu = GameObject.Find("DeathMenu");
        deathMenu.SetActive(false);
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        GetComponent<PlayerInput>().enabled = false;

        helpPanel = GameObject.Find("HelpPanel");

        PlayerStaminabar.maxValue = stamina;
    }

    void Update()
    {
        if (!IsOwner) return; // For NetworkBehaviour

        //Health
        PlayerHPbar.value = hp; //Change the HP slider based on the remaining HP of the player
        HPgradient.Evaluate(PlayerHPbar.normalizedValue);
        GameObject.FindGameObjectWithTag("PlayerSliderFill").GetComponent<Image>().color = HPgradient.Evaluate(PlayerHPbar.normalizedValue);

        hpPercentage = hp * 100 / PlayerHPbar.maxValue;
        hpText.text = hp + " / " + PlayerHPbar.maxValue + " (" + (int)hpPercentage + "%)";


        //Stamina (some of the code is in ThirdPersonController script and StarterAssetsInputs script)
        PlayerStaminabar.value = stamina;

        if (Time.time - lastDrainStaminaTime > refillStaminaDelay)
        {
            stamina += Time.deltaTime * staminaRefillRate;
            stamina = Mathf.Clamp(stamina, 0, (int)PlayerStaminabar.maxValue);
        }

        

        #region Pause Menu & Slow Motion Effect Upon Death
        //Pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && hp > 0 && !helpPanel.activeSelf)
        {
            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                GetComponent<PlayerInput>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                GetComponent<PlayerInput>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }                
        }

        //slow motion effect
        if (hp <= 0 && canExecuteSlowMo)
        {
            slowMoTimer += Time.deltaTime;

            if (slowMoTimer < 5 * Time.timeScale) // DeltaTime is affected by timescale so i multiply it with the current timescale so that i get the real-time
                Time.timeScale = 0.8f;
            else
            {
                GetComponent<PlayerInput>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                deathMenu.SetActive(true);
                Time.timeScale = 0f;
                slowMoTimer = 0;
                canExecuteSlowMo = false;
            }
        }
        #endregion

        
    }


    //Function called when player's weapon hits the enemy in order for the enemy to take damage
    public void TakeDamage(int damageValue)
    {
        hp -= damageValue;//Decrease player health based on the damage dealt by the enemy
        hp = Mathf.Clamp(hp, 0, (int)PlayerHPbar.maxValue); //clamp hp between 0 and maxHp so that i won't go below 0
        if (hp <= 0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            dead = true;
        }

    }

    //Function called to drain stamina when attacking with weapon or sprinting or jumping. Weapon attack stamina is drained based
    //on the attack move type and the weapon's weight
    public void DrainStamina(float drainValue)
    {
        stamina -= drainValue;
        stamina = Mathf.Clamp(stamina, 0, (int)PlayerStaminabar.maxValue);

        lastDrainStaminaTime = Time.time;
    }


    public void UpgradeStamina(float stm)
    {
        PlayerStaminabar.maxValue += stm;
    }

    public void UpgradeHealth(float health)
    {
        PlayerHPbar.maxValue += health;
        hp = (int)PlayerHPbar.maxValue;
    }


    //Function called when player's weapon hits the enemy in order for the enemy to take damage
    public void RestoreHealth(int hpValue)
    {
        hp += hpValue;//Decrease player health based on the damage dealt by the enemy
        hp = Mathf.Clamp(hp, 0, (int)PlayerHPbar.maxValue); //clamp hp between 0 and maxHp so that i won't go below 0
    }
}