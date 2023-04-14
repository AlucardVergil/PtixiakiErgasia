using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerNoiseLevels : MonoBehaviour
{
    public float walkNoiseVolume;
    public float runNoiseVolume;
    public float jumpNoiseVolume;
    public float landNoiseVolume;
    public float weaponNoiseVolume;
    public float spellNoiseVolume;

    [HideInInspector] public float noiseLevel;
    private float fallTimer;

    private StarterAssetsInputs _input;
    private ThirdPersonController controller;
    private Animator playerAnim;

    private bool crouch;



    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        controller = GetComponent<ThirdPersonController>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        noiseLevel = 0;


        
        if (Input.GetKeyDown(KeyCode.C))
        {
            crouch = !crouch; //note need to add crouch animation etc
        }
        
        

        if (!_input.sprint && _input.move != Vector2.zero && controller.Grounded && !crouch)
            PlayerNoiseMade(walkNoiseVolume);

        if (_input.sprint && _input.move != Vector2.zero && controller.Grounded)
            PlayerNoiseMade(runNoiseVolume);

        if (_input.jump && !controller.Grounded) //this happens in only one frame when your feet leave the ground
            PlayerNoiseMade(jumpNoiseVolume);

        if (Input.GetMouseButtonUp(0))
            PlayerNoiseMade(weaponNoiseVolume);

        //Time how long the player was freefalling to find out from how high he fell, in order to make more noise when landing
        if (playerAnim.GetBool("FreeFall"))
            fallTimer += Time.deltaTime;
        else if (fallTimer > 0 && controller.Grounded) //if (controller.Grounded && playerAnim.GetBool("FreeFall")) //only happens in one frame when lands
        {
            float landingNoise = landNoiseVolume + (fallTimer * 10);
            landingNoise = Mathf.Clamp(landingNoise, landNoiseVolume, 100);

            PlayerNoiseMade(landingNoise);

            fallTimer = 0;
        }
               
        

        /*
        Debug.Log(Time.time + " Jump " + _input.jump + "\n" +
            Time.time + " Grounded " + controller.Grounded + "\n" +
            Time.time + " FreeFall " + playerAnim.GetBool("FreeFall") + "\n\n");
        */
    }


    public void PlayerNoiseMade(float noiseMade)
    {
        noiseLevel = noiseMade;
    }    
}