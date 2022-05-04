using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D projectileRB;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    float secondsToDestroy = 3.0f;

    private float damage;
    private bool fromPlayer;

    public bool FromPlayer { get => fromPlayer; set => fromPlayer = value; }
    public float Damage { set => damage = value; }

    private void Awake()
    {
        // Destroy after few seconds
        Destroy(gameObject, secondsToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (FromPlayer == true)
        {

            //was a enemy hit?

            if (col.gameObject.layer == 11)
            {

                //if yes apply damage

                col.gameObject.GetComponent<Enemy>().TakeDamage(damage, false);
            }

            //Destroy after hit
            Destroy(gameObject);

        }
        else
        {
            //when Projectile from Enemy
            int layer = col.gameObject.layer;
            switch (layer)
            {
                //If Projectile hits a player, player takes damage
                case 8:
                    Player[] colPlayer = col.gameObject.GetComponentsInChildren<Player>();
                    if (colPlayer.Length != 0)
                    {
                        if (PlayerData.PlayerIsMelee)
                        {
                            colPlayer[0].PlayerTakeDamage(damage);
                        }
                        else
                        {
                            colPlayer[1].PlayerTakeDamage(damage);
                        }
                        Destroy(gameObject);
                    }

                    break;

                //If Projectile hits a wall or floor it is destroyed
                case 9:
                    Destroy(gameObject);
                    break;
            }

        }

    }


    public void Shoot(Vector2 direction)
    {
        projectileRB.AddForce(direction * bulletSpeed);
    }
}
