using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviourBase : StateMachineBehaviour
{

    protected Animator myAnimator;
    protected Transform playerPos,teleport1,teleport2;
    protected SpawnProjectile mySpawn,pillarSpawn;
    protected Collider2D pillarCollider;
    protected AudioSource myExplosionSource;
    protected GameObject myGO;

    //initialise all scene-data in a boss behaviour
    public void InitBossState(Animator animator, Transform pPos, SpawnProjectile spawn, Collider2D pillar, AudioSource myExplosionSource, SpawnProjectile pillarSpawn, Transform teleport1, Transform teleport2, GameObject go)
    {
        myAnimator = animator;
        playerPos = pPos;
        mySpawn = spawn;
        this.pillarCollider = pillar;
        this.myExplosionSource = myExplosionSource;
        this.pillarSpawn = pillarSpawn;
        this.teleport1 = teleport1;
        this.teleport2 = teleport2;
        this.myGO = go;
    }
}