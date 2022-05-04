using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField]
    SpawnProjectile mySpawn;
    [SerializeField]
    GameObject myAnimationParent;
    [SerializeField]
    Animator myAnimator;
    [SerializeField]
    float shootingCooldown, rangedDamage, shootAnimationLength;
    float shootingTimer = 0;
    private bool lookingLeft = true;
    public override void Routine()
    {
        base.Routine();
        //if player ever moves to the other side of shooter, change facing.
        if (lookingLeft && playerM.transform.position.x > this.transform.position.x)
        {
            myAnimationParent.transform.localScale = new Vector3(-1, 1, 1);
            lookingLeft = false;
        }
        if (!lookingLeft && playerM.transform.position.x < this.transform.position.x)
        {
            myAnimationParent.transform.localScale = new Vector3(1, 1, 1);
            lookingLeft = true;
        }

        shootingTimer += Time.deltaTime;
        //after given time, play animation
        if (shootingTimer >= shootingCooldown - shootAnimationLength)
        {
            myAnimator.SetBool("IsAttacking", true);

        }
        //after more given time call Shoot() and reset timer and animation.
        if (shootingTimer >= shootingCooldown)
        {
            Shoot();
            shootingTimer = 0;
            myAnimator.SetBool("IsAttacking", false);
        }
    }
    public override void Respawn()
    {
        base.Respawn();
        shootingTimer = 0;
        if (!lookingLeft)
        {
            myAnimationParent.transform.localScale = new Vector3(1, 1, 1);
            lookingLeft = true;
        }
    }
    /// <summary>
    /// Shoots a projectile at playerM.position
    /// </summary>
    private void Shoot()
    {
        Vector2 angleVector = new Vector2(playerM.transform.position.x - this.transform.position.x, playerM.transform.position.y - this.transform.position.y).normalized;
        mySpawn.SpawnOneProjectile(rangedDamage, false, angleVector);
    }
}
