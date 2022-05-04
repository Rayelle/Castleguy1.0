using UnityEngine;


public class RangedWeapon : Weapon
{
    [SerializeField]
    Player myRangedPlayer;





    public override void Attack(bool lookingright)
    {

        //spawning Projectile in Direktion Player is looking

        if (lookingright)
        {
            myRangedPlayer.GetComponent<SpawnProjectile>().SpawnOneProjectile(damage, true, Vector2.right);
        }
        else myRangedPlayer.GetComponent<SpawnProjectile>().SpawnOneProjectile(damage, true, Vector2.left);





    }
}
