using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourSpawn : BossBehaviourBase
{
    float fireballTimer, fireballCD = 0.3f, animationTimer, colliderTimer;
    bool goToTeleport1 = true;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //reset timers and activate PillarCollider and ExplosionSound
        animationTimer = 0;
        fireballTimer = 0;
        pillarCollider.enabled = true;
        myExplosionSource.Play();
        //if Player is not far away, shoots three Projectiles from pillarSpawn to player. One Projectile shoots straight at the player, one shoots slightly above and one slightly below the player.
        if(new Vector2(playerPos.position.x - mySpawn.transform.position.x, playerPos.position.y - mySpawn.transform.position.y).magnitude <= 15)
        {
            //Switch between going to Teleport1 and going to Teleport2
            if (goToTeleport1)
            {
                goToTeleport1 = false;
                myGO.transform.position = teleport1.position;
            }
            else
            {
                goToTeleport1 = true;
                myGO.transform.position = teleport2.position;
            }
            //shoot three projectiles
            Vector2 angleVector = new Vector2(playerPos.position.x - pillarSpawn.transform.position.x, playerPos.position.y - pillarSpawn.transform.position.y).normalized;
            pillarSpawn.SpawnOneProjectile(2, false, angleVector);
            angleVector = new Vector2(playerPos.position.x - pillarSpawn.transform.position.x, playerPos.position.y - pillarSpawn.transform.position.y);
            angleVector += Vector2.up;
            pillarSpawn.SpawnOneProjectile(2, false, angleVector.normalized);
            angleVector = new Vector2(playerPos.position.x - pillarSpawn.transform.position.x, playerPos.position.y - pillarSpawn.transform.position.y);
            angleVector -= Vector2.up;
            pillarSpawn.SpawnOneProjectile(2, false, angleVector.normalized);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Jump to next state when animation is over.
        animationTimer += Time.deltaTime;
        if (animationTimer >= stateInfo.length)
        {
            myAnimator.SetTrigger("SpawnDone");
        }
        //Remove Huge BoxCollider after the Firepillar cant be seen anymore.
        colliderTimer += Time.deltaTime;
        if (colliderTimer >= 0.2)
        {
            pillarCollider.enabled = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

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
