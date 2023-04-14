//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Custom Assets/StarterAssets/InputSystem/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f62a4b92-ef5e-4175-8f4c-c9075429d32c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6bc1aaf4-b110-4ff7-891e-5b9fe6f32c4d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""2690c379-f54d-45be-a724-414123833eb4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8c4abdf8-4099-493a-aa1a-129acec7c3df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""980e881e-182c-404c-8cbf-3d09fdb48fef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""44d6773f-a22b-4fb1-980c-a5479af024a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""9d1467fb-3ff3-44da-a31f-0002909b30d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnRight"",
                    ""type"": ""Button"",
                    ""id"": ""8186834c-b9d3-4f16-88be-868c6266deb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f5260181-c0b2-4193-b405-6ecbc5cfc099"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenGameMenu"",
                    ""type"": ""Button"",
                    ""id"": ""5dfafb80-d126-4978-a0d1-ddc7d8d91231"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""14e4409c-e3d7-47e0-8cf2-2adc28289e96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FreezeSpell"",
                    ""type"": ""Button"",
                    ""id"": ""c98f9e8a-3a61-4a25-8d24-6ba994488117"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WallSpell"",
                    ""type"": ""Button"",
                    ""id"": ""e3a52220-0a25-46fe-bdd0-157e1a19ca27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ProjectileSpell"",
                    ""type"": ""Button"",
                    ""id"": ""1e3cae03-c33d-4091-b61f-2b0b3b7a107d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Kick"",
                    ""type"": ""Button"",
                    ""id"": ""a7e4a770-68e2-4af1-a660-13cff450b514"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b7594ddb-26c9-4ba2-bd5a-901468929edc"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2063a8b5-6a45-43de-851b-65f3d46e7b58"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""64e4d037-32e1-4fb9-80e4-fc7330404dfe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0fce8b11-5eab-4e4e-a741-b732e7b20873"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7bdda0d6-57a8-47c8-8238-8aecf3110e47"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bb94b405-58d3-4998-8535-d705c1218a98"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""929d9071-7dd0-4368-9743-6793bb98087e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""28abadba-06ff-4d37-bb70-af2f1e35a3b9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""45f115b6-9b4f-4ba8-b500-b94c93bf7d7e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e2f9aa65-db06-4c5b-a2e9-41bc8acb9517"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed66cbff-2900-4a62-8896-696503cfcd31"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d171b6-19d8-47a6-ba3a-71b6a8e7b3c0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),StickDeadzone,ScaleVector2(x=300,y=300)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bd55a0b-761e-4ae4-89ae-8ec127e08a29"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f973413-5e27-4239-acee-38c4a63feeba"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc65b89f-9bd3-43fb-92af-d0d87ba5faa4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8fcd86e-dcfd-4f88-8e93-b638cdbf3320"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7bb3211-00a6-464e-97bf-98ec4e2fc549"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""367651b2-76e3-4676-b0d3-9f4ad3d1eb8b"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c46bc08-2986-4401-8f26-a4da1e6eb2ae"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""LockOnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43b2655c-acc6-491a-94dd-4751e7b9514f"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""LockOnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21e5c313-8f20-494c-a3ab-84995c3af6eb"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""OpenGameMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dd40140-15ba-4081-9137-fb688074cd10"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85f8ad06-35d0-4589-ab10-9185e20164a8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""FreezeSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5aed8138-0d10-4075-9d64-c4de88128c3e"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""WallSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""7f744df4-0e2f-458b-ad5e-6810e33a4585"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ProjectileSpell"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""9736dc2e-0c23-4bad-9e10-18e240dc7822"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ProjectileSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""7169675c-a2c4-46c6-9b63-574db78c558a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""ProjectileSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fc9bb3c2-6059-4917-b005-38c8226b9382"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Kick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GameMenuUI"",
            ""id"": ""080815f3-1a22-46d5-95b9-bebb9cb2bafb"",
            ""actions"": [
                {
                    ""name"": ""OpenGameMenu"",
                    ""type"": ""Button"",
                    ""id"": ""066bd6fe-da2a-40b5-8960-2a390038ba5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""540c61fb-4ee1-4d9c-9e8e-3e7b79c313e9"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""OpenGameMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox Controller"",
            ""bindingGroup"": ""Xbox Controller"",
            ""devices"": []
        },
        {
            ""name"": ""PS4 Controller"",
            ""bindingGroup"": ""PS4 Controller"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_LockOn = m_Player.FindAction("LockOn", throwIfNotFound: true);
        m_Player_LockOnRight = m_Player.FindAction("LockOnRight", throwIfNotFound: true);
        m_Player_LockOnLeft = m_Player.FindAction("LockOnLeft", throwIfNotFound: true);
        m_Player_OpenGameMenu = m_Player.FindAction("OpenGameMenu", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_FreezeSpell = m_Player.FindAction("FreezeSpell", throwIfNotFound: true);
        m_Player_WallSpell = m_Player.FindAction("WallSpell", throwIfNotFound: true);
        m_Player_ProjectileSpell = m_Player.FindAction("ProjectileSpell", throwIfNotFound: true);
        m_Player_Kick = m_Player.FindAction("Kick", throwIfNotFound: true);
        // GameMenuUI
        m_GameMenuUI = asset.FindActionMap("GameMenuUI", throwIfNotFound: true);
        m_GameMenuUI_OpenGameMenu = m_GameMenuUI.FindAction("OpenGameMenu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_LockOn;
    private readonly InputAction m_Player_LockOnRight;
    private readonly InputAction m_Player_LockOnLeft;
    private readonly InputAction m_Player_OpenGameMenu;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_FreezeSpell;
    private readonly InputAction m_Player_WallSpell;
    private readonly InputAction m_Player_ProjectileSpell;
    private readonly InputAction m_Player_Kick;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @LockOn => m_Wrapper.m_Player_LockOn;
        public InputAction @LockOnRight => m_Wrapper.m_Player_LockOnRight;
        public InputAction @LockOnLeft => m_Wrapper.m_Player_LockOnLeft;
        public InputAction @OpenGameMenu => m_Wrapper.m_Player_OpenGameMenu;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @FreezeSpell => m_Wrapper.m_Player_FreezeSpell;
        public InputAction @WallSpell => m_Wrapper.m_Player_WallSpell;
        public InputAction @ProjectileSpell => m_Wrapper.m_Player_ProjectileSpell;
        public InputAction @Kick => m_Wrapper.m_Player_Kick;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @LockOn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOn;
                @LockOnRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnRight;
                @LockOnRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnRight;
                @LockOnRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnRight;
                @LockOnLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnLeft;
                @LockOnLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnLeft;
                @LockOnLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLockOnLeft;
                @OpenGameMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenGameMenu;
                @OpenGameMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenGameMenu;
                @OpenGameMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenGameMenu;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @FreezeSpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFreezeSpell;
                @FreezeSpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFreezeSpell;
                @FreezeSpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFreezeSpell;
                @WallSpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWallSpell;
                @WallSpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWallSpell;
                @WallSpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWallSpell;
                @ProjectileSpell.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProjectileSpell;
                @ProjectileSpell.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProjectileSpell;
                @ProjectileSpell.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProjectileSpell;
                @Kick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKick;
                @Kick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKick;
                @Kick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKick;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @LockOnRight.started += instance.OnLockOnRight;
                @LockOnRight.performed += instance.OnLockOnRight;
                @LockOnRight.canceled += instance.OnLockOnRight;
                @LockOnLeft.started += instance.OnLockOnLeft;
                @LockOnLeft.performed += instance.OnLockOnLeft;
                @LockOnLeft.canceled += instance.OnLockOnLeft;
                @OpenGameMenu.started += instance.OnOpenGameMenu;
                @OpenGameMenu.performed += instance.OnOpenGameMenu;
                @OpenGameMenu.canceled += instance.OnOpenGameMenu;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @FreezeSpell.started += instance.OnFreezeSpell;
                @FreezeSpell.performed += instance.OnFreezeSpell;
                @FreezeSpell.canceled += instance.OnFreezeSpell;
                @WallSpell.started += instance.OnWallSpell;
                @WallSpell.performed += instance.OnWallSpell;
                @WallSpell.canceled += instance.OnWallSpell;
                @ProjectileSpell.started += instance.OnProjectileSpell;
                @ProjectileSpell.performed += instance.OnProjectileSpell;
                @ProjectileSpell.canceled += instance.OnProjectileSpell;
                @Kick.started += instance.OnKick;
                @Kick.performed += instance.OnKick;
                @Kick.canceled += instance.OnKick;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // GameMenuUI
    private readonly InputActionMap m_GameMenuUI;
    private IGameMenuUIActions m_GameMenuUIActionsCallbackInterface;
    private readonly InputAction m_GameMenuUI_OpenGameMenu;
    public struct GameMenuUIActions
    {
        private @PlayerControls m_Wrapper;
        public GameMenuUIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenGameMenu => m_Wrapper.m_GameMenuUI_OpenGameMenu;
        public InputActionMap Get() { return m_Wrapper.m_GameMenuUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameMenuUIActions set) { return set.Get(); }
        public void SetCallbacks(IGameMenuUIActions instance)
        {
            if (m_Wrapper.m_GameMenuUIActionsCallbackInterface != null)
            {
                @OpenGameMenu.started -= m_Wrapper.m_GameMenuUIActionsCallbackInterface.OnOpenGameMenu;
                @OpenGameMenu.performed -= m_Wrapper.m_GameMenuUIActionsCallbackInterface.OnOpenGameMenu;
                @OpenGameMenu.canceled -= m_Wrapper.m_GameMenuUIActionsCallbackInterface.OnOpenGameMenu;
            }
            m_Wrapper.m_GameMenuUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenGameMenu.started += instance.OnOpenGameMenu;
                @OpenGameMenu.performed += instance.OnOpenGameMenu;
                @OpenGameMenu.canceled += instance.OnOpenGameMenu;
            }
        }
    }
    public GameMenuUIActions @GameMenuUI => new GameMenuUIActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("Xbox Controller");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    private int m_PS4ControllerSchemeIndex = -1;
    public InputControlScheme PS4ControllerScheme
    {
        get
        {
            if (m_PS4ControllerSchemeIndex == -1) m_PS4ControllerSchemeIndex = asset.FindControlSchemeIndex("PS4 Controller");
            return asset.controlSchemes[m_PS4ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnLockOnRight(InputAction.CallbackContext context);
        void OnLockOnLeft(InputAction.CallbackContext context);
        void OnOpenGameMenu(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnFreezeSpell(InputAction.CallbackContext context);
        void OnWallSpell(InputAction.CallbackContext context);
        void OnProjectileSpell(InputAction.CallbackContext context);
        void OnKick(InputAction.CallbackContext context);
    }
    public interface IGameMenuUIActions
    {
        void OnOpenGameMenu(InputAction.CallbackContext context);
    }
}
