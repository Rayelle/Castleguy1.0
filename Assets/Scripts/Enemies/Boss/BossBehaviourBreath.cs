using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourBreath : BossBehaviourBase
{
    private float animationTimer;
    private float fireballTimer, fireballCD= 0.75f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //reset timers
        animationTimer = 0;
        fireballTimer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Jump to next state when animation is over.
        animationTimer += Time.deltaTime;
        if (animationTimer >= stateInfo.length)
        {
            myAnimator.SetTrigger("BreathDone");
        }
        //Shoot one Fireball when animation is at the right moment.
        fireballTimer += Time.deltaTime;
        if (fireballTimer >= fireballCD)
        {
            //check if player is close before shooting at him.
            if (new Vector2(playerPos.position.x - mySpawn.transform.position.x, playerPos.position.y - mySpawn.transform.position.y).magnitude <= 15)
            {
                Vector2 angleVector = new Vector2(playerPos.position.x - mySpawn.transform.position.x, playerPos.position.y - mySpawn.transform.position.y).normalized;
                mySpawn.SpawnOneProjectile(4, false, angleVector);
                fireballTimer = 0;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
