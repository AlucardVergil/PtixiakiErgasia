using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


//This class is not added as a component. It is only used as an inherited class for ThirdPersonController, in order to separate 
//the code for the Camera Lock On inside a different script, for orgazination purposes
public class CameraLockOnHandler : MonoBehaviour
{
    public Transform targetTransform;
    public float maximumLockOnDistance = 30;    
    [HideInInspector] public Transform nearestLockOnTarget;
    [HideInInspector] public Transform currentLockOnTarget;

    [HideInInspector] public Transform leftLockTarget;
    [HideInInspector] public Transform rightLockTarget;

    protected List<CharacterManager> availableTargets = new List<CharacterManager>();
    protected GameObject _mainCamera;
    protected StarterAssetsInputs _input;    

    [Header("Player Camera Root to rotate for lock on target, \nbecause you can't rotate the maincamera directly.")]
    public Transform playerCameraRoot;

    public GameObject playerLockOnTransform;

    protected void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }


    protected void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }


    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);

        availableTargets.Clear();

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager enemy = colliders[i].GetComponent<CharacterManager>();

            if (enemy != null)
            {
                Vector3 lockTargetDirection = enemy.transform.position - targetTransform.position;
                float distanceFromTarget = Vector3.Distance(targetTransform.position, enemy.transform.position);
                float viewableAngle = Vector3.Angle(lockTargetDirection, _mainCamera.transform.forward);
                
                if (enemy.transform.root != targetTransform.transform.root
                    && viewableAngle > -50 && viewableAngle < 50
                    && distanceFromTarget <= maximumLockOnDistance && enemy.GetComponent<Enemy>().hp > 0) //&& !availableTargets.Contains(character))
                {
                    if (!Physics.Linecast(playerLockOnTransform.transform.position, enemy.lockOnTransform.position))
                        availableTargets.Add(enemy);
                }
                /*
                else
                {
                    availableTargets.Remove(character);
                } 
                */
            }
        }

        //Debug.Log(Time.time + " Count " + availableTargets.Count);

        for (int k = 0; k < availableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(targetTransform.position, availableTargets[k].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = availableTargets[k].lockOnTransform;
            }

            //Switch Lock On Target Left/Right
            if (_input.lockOnFlag)
            {
                Vector3 relativeEnemyPosition = currentLockOnTarget.InverseTransformPoint(availableTargets[k].transform.position);
                var distanceFromLeftTarget = currentLockOnTarget.transform.position.x - availableTargets[k].transform.position.x;
                var distanceFromRightTarget = currentLockOnTarget.transform.position.x + availableTargets[k].transform.position.x;

                if (relativeEnemyPosition.x > 0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                    leftLockTarget = availableTargets[k].lockOnTransform;
                }

                if (relativeEnemyPosition.x < 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget = distanceFromRightTarget;
                    rightLockTarget = availableTargets[k].lockOnTransform;
                }
            }
        }
    }


    public void CameraRotationLockOn()
    {        
        if (currentLockOnTarget.GetComponentInParent<Enemy>().hp > 0)
        {
            //float velocity = 0;

            //rotate player
            Vector3 dir = currentLockOnTarget.position - transform.position;
            dir.Normalize();
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = targetRotation;

            //rotate camera
            dir = currentLockOnTarget.position - _mainCamera.transform.position;
            dir.Normalize();

            targetRotation = Quaternion.LookRotation(dir);
            Vector3 eulerAngle = targetRotation.eulerAngles;
            eulerAngle.y = 0;
            playerCameraRoot.transform.localEulerAngles = eulerAngle;

            playerCameraRoot.transform.position = new Vector3(playerCameraRoot.transform.position.x, 
                1.6f, playerCameraRoot.transform.position.z);
        }
        else //if target dies change target
        {   
            _input.lockOnInput = false;
            _input.lockOnFlag = false;
            ClearLockOnTargets();

            HandleLockOn();
            if (nearestLockOnTarget != null)
            {
                currentLockOnTarget = nearestLockOnTarget;
                _input.lockOnFlag = true;
            }
        }        
    }


    public void ClearLockOnTargets()
    {
        availableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;

        playerCameraRoot.transform.position = new Vector3(playerCameraRoot.transform.position.x, 
            1.375f, playerCameraRoot.transform.position.z);        
    }



}