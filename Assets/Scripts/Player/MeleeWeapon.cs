using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    private float range;

    
    public override void Attack(bool lookingRight)
    {
        

        Collider2D[] hitColliders;


        //Checking direction Player is looking at, adding everything hit to Array
        if (lookingRight)
        {
            hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.gameObject.transform.position.x + 0.7f, this.gameObject.transform.position.y), range * 0.5f);
        }
        else
        {
            hitColliders = Physics2D.OverlapCircleAll(new Vector2(this.gameObject.transform.position.x - 0.7f, this.gameObject.transform.position.y), range * 0.5f);

        }

        //Checking if "Sword Slash" (Overlapsphere) hit a Enemy

        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject enemyGO = hitColliders[i].gameObject;

            if (enemyGO.layer == 11)
            {

                //if Enemy is hit he takes damage

                enemyGO.GetComponent<Enemy>().TakeDamage(damage, true);

            }

        }
    }








}

