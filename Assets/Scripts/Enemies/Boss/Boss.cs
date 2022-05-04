using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    [SerializeField]
    Animator myAnimator;
    [SerializeField]
    Transform playerPos,teleport1,teleport2;
    [SerializeField]
    SpawnProjectile mySpawn,pillarSpawn;
    [SerializeField]
    Collider2D pillarCollider,myCollider2D;
    [SerializeField]
    Rigidbody2D pillarRB;
    [SerializeField]
    AudioSource myExplosionSource;
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    GameObject myGO;

    private float maxHitpoints;
    private void Start()
    {
        //Look for each BossBehavoiur in myAnimator and Initialise all scene-data in each BossBehaviour.
        foreach(BossBehaviourBase state in myAnimator.GetBehaviours<BossBehaviourBase>())
        {
            state.InitBossState(myAnimator, playerPos, mySpawn,pillarCollider,myExplosionSource,pillarSpawn,teleport1,teleport2, myGO) ;
        }
        maxHitpoints = hitpoints;
        
    }
    private void Update()
    {
        //Check to see if boss dies.
        if (hitpoints<= 0)
        {
            BossKill();
        }
        //If player Dies, boss HP is reset and fight starts anew
        if (PlayerData.Healthpoints <= 0)
        {
            hitpoints = maxHitpoints;
        }
        //Updates the boss Healthbar
        healthBar.value = hitpoints;

        //always turn boss towards player
        if (playerPos.position.x < this.transform.position.x)
        {
            this.transform.localScale = new Vector3(4, 4, 4);
        }
        else
        {
            this.transform.localScale = new Vector3(-4, 4, 4);
        }
    }
    private void BossKill()
    {
        //set boss-animator to dead state, remove damaging colliders and unlock Gate.
        myAnimator.SetTrigger("BossDead");
        myCollider2D.enabled = false;
        pillarCollider.enabled = false;

        pillarRB.constraints = RigidbodyConstraints2D.None;
        pillarRB.AddForce(Vector2.up);
    }
}
