using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourIdle : BossBehaviourBase
{
    private float stateTimer;
    bool breathIsNext;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //reset timer
        stateTimer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Keep track of time, then alternate between going to Breath-state and going to Spawn-state. 
        stateTimer += Time.deltaTime;
        if (stateTimer >= 3)
        {
            if (breathIsNext)
            {
                myAnimator.SetTrigger("IdleToBreath");
                breathIsNext = false;
            }
            else
            {
                myAnimator.SetTrigger("IdleToSpawn");
                breathIsNext = true;
            }
        }
    }
}
