using System;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform rightSpawnPosition;
    [SerializeField]
    private Transform leftSpawnPosition;

    [SerializeField]
    GameObject projectile_Prefab;


    public void SpawnOneProjectile(float damage, bool fromPlayer, Vector2 direction)
    {
        //Check if from Player

        if (fromPlayer == true)
        {
            if (direction == Vector2.right)
            {
                //create new Instance, shoot in direction Player is looking at (right)

                GameObject newProjectile = GameObject.Instantiate(projectile_Prefab, rightSpawnPosition.position, rightSpawnPosition.rotation);
                Projectile newProjectileScript = newProjectile.GetComponent<Projectile>();
                newProjectileScript.FromPlayer = fromPlayer;
                newProjectileScript.Damage = damage;
                newProjectileScript.Shoot(direction);
            }
            else
            {
                //(left)

                GameObject newProjectile = GameObject.Instantiate(projectile_Prefab, leftSpawnPosition.position, leftSpawnPosition.rotation);
                Projectile newProjectileScript = newProjectile.GetComponent<Projectile>();
                newProjectileScript.FromPlayer = fromPlayer;
                newProjectileScript.Damage = damage;
                newProjectileScript.Shoot(direction);
            }

        }

        //when from Enemy

        else
        {
            //arctan(dir.y/ dir.x)*radtodeg
            float angleDeg = (float)Math.Atan(direction.y / direction.x) * Mathf.Rad2Deg;
            Quaternion angle = Quaternion.Euler(0, 0, angleDeg);
            GameObject newProjectile = GameObject.Instantiate(projectile_Prefab, this.gameObject.transform.position, angle);
            Projectile newProjectileScript = newProjectile.GetComponent<Projectile>();
            newProjectileScript.FromPlayer = fromPlayer;
            newProjectileScript.Damage = damage;
            newProjectileScript.Shoot(direction);
        }

    }
}
