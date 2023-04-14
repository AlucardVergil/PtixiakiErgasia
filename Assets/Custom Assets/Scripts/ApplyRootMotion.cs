using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to be added only to the animations in statemachine that i want the movement to be controlled by the animation itself
public class ApplyRootMotion : StateMachineBehaviour
{
    private GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //apply root motion for animation so that the movement is controlled by the animation
        player.GetComponent<Animator>().applyRootMotion = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //disable root motion after animation so that the movement is controlled by the character controller again
        player.GetComponent<Animator>().applyRootMotion = false;
    }
}
