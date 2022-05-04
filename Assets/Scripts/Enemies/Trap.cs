using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Player playerM, playerR;
    [SerializeField]
    private float damage, triggerStayDamageCooldown;
    private float stayTimer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            stayTimer = 0;
            if (PlayerData.PlayerIsMelee)
            {
                playerM.PlayerTakeDamage(damage);
            }
            else
            {
                playerR.PlayerTakeDamage(damage);
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            if (collision.gameObject.layer == 8)
            {
                stayTimer += Time.deltaTime;
                if (stayTimer >= triggerStayDamageCooldown)
                {
                    stayTimer = 0;
                    if (PlayerData.PlayerIsMelee)
                    {
                        playerM.PlayerTakeDamage(damage);
                    }
                    else
                    {
                        playerR.PlayerTakeDamage(damage);
                    }
                }
            }
        }
    }
}
