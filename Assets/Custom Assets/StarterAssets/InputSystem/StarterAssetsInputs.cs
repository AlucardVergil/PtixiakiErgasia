using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

using Unity.Netcode;

namespace StarterAssets
{
	public class StarterAssetsInputs : NetworkBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
        [Space]
        [Space]
        [Space]

        [Header("Variables for player attacking")]
        public bool attack; //Variable for checking when player is attacking
		public bool attackCanHit;
		[HideInInspector] public int damageValue;

        [Header("Variables For Lock On Target")]
        public bool lockOnFlag;
		public bool lockOnInput;
		public bool lockOnRightInput;
		public bool lockOnLeftInput;

		[Header("Menu Panel")]
		public GameObject menu;
        [Header("Inventory Panel")]
        public GameObject inventory; //to items panel

        public PlayerControls playerControls;

        [Header("Variables For Interaction")]
        public float interactionDistance = 5f;
        public LayerMask interactionLayer;
        public Transform crosshair;
        public Camera cam;


        private void Awake()
        {
            
        }


        private void Start()
        {
			inventory.GetComponent<InventoryUI>().itemDetailsPanel.SetActive(false);
            inventory.SetActive(false);
            menu.SetActive(false);			
		}


        private void OnEnable()
        {
            if (playerControls == null)
				playerControls = new PlayerControls();


            #region Player Action Map

            playerControls.Player.Move.performed += i => MoveInput(i);
			playerControls.Player.Move.canceled += i => MoveInput(i);

			playerControls.Player.Look.performed += i => LookInput(i);
			playerControls.Player.Look.canceled += i => LookInput(i);

			playerControls.Player.Jump.performed += i => JumpInput(i);

			playerControls.Player.Sprint.performed += i => SprintInput(i);
			playerControls.Player.Sprint.canceled += i => SprintInput(i);

			playerControls.Player.Attack.performed += i => GetComponent<ComboAttacks>().ExecuteWeaponAttack(i);

			playerControls.Player.LockOn.performed += i => LockOnInput(i);

			playerControls.Player.LockOnLeft.performed += i => LockOnInputLeft(i);
			playerControls.Player.LockOnRight.performed += i => LockOnInputRight(i);

			playerControls.Player.Dash.performed += i => GetComponent<ComboAttacks>().DashSuperSpeedMove();

			playerControls.Player.FreezeSpell.performed += i => GetComponent<FreezeSpellShooter>().ExecuteFreezeSpell();

			playerControls.Player.WallSpell.performed += i => GetComponent<SpawnWallSpell>().ExecuteWallSpell();
			playerControls.Player.Kick.performed += i => GetComponent<SpawnWallSpell>().ExecuteKickWall();

			//playerControls.Player.ProjectileSpell.performed += i => GetComponent<ProjectileShooter>().ExecuteProjectileSpell();

			playerControls.Player.OpenGameMenu.performed += i => OpenGameMenu();

            playerControls.Player.OpenInventory.performed += i => ToggleInventory();

            playerControls.Player.Interact.performed += i => InteractWithObject();

            #endregion

            #region GameMenuUI Action Map

            playerControls.GameMenuUI.OpenGameMenu.performed += i => OpenGameMenu();

            playerControls.GameMenuUI.CloseInventory.performed += i => ToggleInventory();

            #endregion

            playerControls.Enable();
		}


        private void OnDisable()
        {
            playerControls.Disable();
        }





#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		/*public void OnMove(InputValue value)
		{
			Debug.Log(Time.time + " OLD");
			if (!Input.GetKey(KeyCode.LeftControl)) //for dodge
				MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			//when jumping drain 10 stamina
			if (GetComponent<PlayerStats>().stamina > 15 && GetComponent<ThirdPersonController>().Grounded)
			{
				GetComponent<PlayerStats>().DrainStamina(15);
				JumpInput(value.isPressed);
			}
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);			
		}*/

		//Function for attacking which is triggered when you click the left mount button. 
		//Left click was set in the StarterAssets Input System Asset inside the Input System folder.
		//I set an Input Action Map and named it Attack, so "OnAttack" triggers the Attack Input Action Map
		/*public void OnAttack(InputValue value)
        {
			//If left mouse button is clicked it calls the AttackInput() and passes True as the parameter
			//which is then saved in the attack bool
			if (!GetComponent<SheathWeapon>().sheathBool)
				AttackInput(value.isPressed);
			Debug.Log("Attack " + value.isPressed);
		}

		


		//lock on
		public void OnLockOn(InputValue value)
        {
			LockOnInput(value.isPressed);
		}

		public void OnLockOnRight(InputValue value)
		{
			LockOnInputRight(value.isPressed);
		}

		public void OnLockOnLeft(InputValue value)
		{
			LockOnInputLeft(value.isPressed);
		}*/

		//public void OnOpenGameMenu()
		//{
		//OpenGameMenu();
		//}




#endif




		/*public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} */

		public void MoveInput(InputAction.CallbackContext context)
		{
            if (!IsOwner) return; // For NetworkBehaviour

            if (!Input.GetKey(KeyCode.LeftControl)) //for dodge
				move = context.ReadValue<Vector2>();
		}

		public void LookInput(InputAction.CallbackContext context)
		{
            if (!IsOwner) return; // For NetworkBehaviour
			
            if (cursorInputForLook)
				look = context.ReadValue<Vector2>();
		}

		public void JumpInput(InputAction.CallbackContext context)
		{
			if (!IsOwner) return; // For NetworkBehaviour

			//when jumping drain 15 stamina
			if (GetComponent<PlayerStats>().stamina > 15 && GetComponent<ThirdPersonController>().Grounded)
			{
				GetComponent<PlayerStats>().DrainStamina(15);
				jump = context.performed;
			}
		}


		//changed this from sprint = !sprint to fix sprint bug when opening and closing menus
		public void SprintInput(InputAction.CallbackContext context)
		{
            if (!IsOwner) return; // For NetworkBehaviour

            if (context.phase == InputActionPhase.Performed)
            {
                // Button pressed
                sprint = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                // Button released
                sprint = false;
            }
        }


        /*//function for setting attack state
		public void AttackInput(bool newAttackState)
		{
			//Don't attack with sword when aiming
			if (!Input.GetMouseButton(1))
				attack = newAttackState;
		}*/


        //Lock On
        public void LockOnInput(InputAction.CallbackContext context)
        {
            if (!IsOwner) return; // For NetworkBehaviour

            lockOnInput = context.performed; 
			 var cameraHandler = GetComponent<ThirdPersonController>();

			if (lockOnInput && !lockOnFlag)
            {				
				lockOnInput = false;							
				cameraHandler.HandleLockOn();
				if (cameraHandler.nearestLockOnTarget != null)
                {
					cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
					lockOnFlag = true;
                }
            }
			else if (lockOnInput && lockOnFlag)
            {
				lockOnInput = false;
				lockOnFlag = false;
				cameraHandler.ClearLockOnTargets();
			}


			//Lock On Switch Targets Left/Right
			if (lockOnFlag && lockOnLeftInput)
            {
				lockOnLeftInput = false;
				cameraHandler.HandleLockOn();
				if (cameraHandler.leftLockTarget != null)
                {
					cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
			}

			if (lockOnFlag && lockOnRightInput)
			{
				lockOnRightInput = false;
				cameraHandler.HandleLockOn();
				if (cameraHandler.rightLockTarget != null)
				{
					cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
				}
			}

		}	
		

		public void LockOnInputRight(InputAction.CallbackContext context)
        {
            if (!IsOwner) return; // For NetworkBehaviour

            lockOnRightInput = context.performed;
			LockOnInput(context);
		}


		public void LockOnInputLeft(InputAction.CallbackContext context)
		{
            if (!IsOwner) return; // For NetworkBehaviour

            lockOnLeftInput = context.performed;
			LockOnInput(context);
		}


		public void OpenGameMenu()
        {
            if (!IsOwner) return;

            if (menu.activeSelf)
			{
				Time.timeScale = 1f;
				menu.SetActive(false);
				//GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				playerControls.Player.Enable();
				playerControls.GameMenuUI.Disable();
			}
			else
			{
				Time.timeScale = 0f;
				menu.SetActive(true);
				//GetComponent<PlayerInput>().SwitchCurrentActionMap("GameMenuUI");
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				playerControls.Player.Disable();
				sprint = false;
				playerControls.GameMenuUI.Enable();
			}
		}


		public void ToggleInventory()
		{
			if (!IsOwner) return;

            if (inventory.activeSelf)
            {
				//Close inventory item menu panel and item name panel when closing inventory
				InventoryItem item = inventory.GetComponent<InventoryUI>().GetComponentInChildren<InventoryItem>();
				if (item != null)	
					item.DestroyOptionsMenuAndItemNamePanel();


                inventory.GetComponent<InventoryUI>().itemDetailsPanel.SetActive(false);
                inventory.SetActive(false);
                //GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                Cursor.lockState = CursorLockMode.Locked;
                playerControls.Player.Enable();
                playerControls.GameMenuUI.Disable();
            }
            else
            {
                inventory.SetActive(true);
                //GetComponent<PlayerInput>().SwitchCurrentActionMap("GameMenuUI");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerControls.Player.Disable();
                playerControls.GameMenuUI.Enable();
            }
        }


		public void InteractWithObject()
		{
            if (!IsLocalPlayer) return; // For NetworkBehaviour

            // Raycast from the crosshair position forward
            //Ray ray = new Ray(crosshair.position, crosshair.forward);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.51f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionLayer))
            {
                // Check if the raycast hit an item drop
                if (hit.collider.TryGetComponent<ItemDrop>(out var itemDrop))
                {
                    itemDrop.PickUp();

                    // Highlight the item drop or provide visual feedback

                    // Perform any other actions or effects related to starting the interaction
                }
            }
        }	




        //Function that is called as an event from within the sword slash animation asset, at the end of the sword swing frame
        //in order for the enemy to take damage only when collider hits AND the player has swang the sword.
        //Also attack animation starts when you click left mouse button. Transition back from attack animation is set
        //to on exit time so it exits after the animation finishes. This line is to set the attack boolean variable to
        //false right after it finishes the swing in the attack animation so that it won't play again.
        public void CanAttackHit(AnimationEvent animationEvent)
        {
            if (!IsOwner) return; // For NetworkBehaviour

            if (animationEvent.intParameter == 0)
            {
				attackCanHit = false;
				//GetComponent<ComboAttacks>().maxComboDelay = animationEvent.floatParameter;				
			}				
			else
            {
				attackCanHit = true;
				//This is the damage value of each animation separately, which is also added
				//with the current weapon's damage value inside WeaponCollider script
				damageValue = (int)animationEvent.floatParameter;
			}				
		}
		


		private void OnApplicationFocus(bool hasFocus)
		{
            if (!IsOwner) return; // For NetworkBehaviour

            SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
            if (!IsOwner) return; // For NetworkBehaviour

            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}