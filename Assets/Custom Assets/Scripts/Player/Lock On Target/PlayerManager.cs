using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public Animator _playerAnim;
    [HideInInspector] public CharacterController _characterController;
    [HideInInspector] public ThirdPersonController _thirdPersonController;
    [HideInInspector] public StarterAssetsInputs _input;
    [HideInInspector] public PlayerInput _playerInput;
    [HideInInspector] public AudioSource _playerAudioSource;
    [HideInInspector] public PlayerStats _playerStats;
    [HideInInspector] public PlayerNoiseLevels _playerNoiseLevels;
    [HideInInspector] public SheathWeapon _playerWeaponsManager;
    [HideInInspector] public ComboAttacks _playerWeaponAttacks;



    private void Awake()
    {
        _playerAnim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
        _playerAudioSource = GetComponent<AudioSource>();
        _playerStats = GetComponent<PlayerStats>();
        _playerNoiseLevels = GetComponent<PlayerNoiseLevels>();
        _playerWeaponsManager = GetComponent<SheathWeapon>();
        _playerWeaponAttacks = GetComponent<ComboAttacks>();
    }
}
