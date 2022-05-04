using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    [SerializeField]
    AudioSource myAS1, myAS2;
    [SerializeField]
    protected Rigidbody2D myRB;
    [SerializeField]
    SpriteRenderer mySpriteRenderer;
    [SerializeField]
    AudioClip getHitSoundMelee, getHitSoundRanged, dieSound;
    [SerializeField]
    protected float hitpoints;
    [SerializeField]
    private float damage, triggerStayDamageCooldown;
    [SerializeField]
    Vector2 knockbackStrength;
    [SerializeField]
    protected Player playerM, playerR;
    private bool isKilled,isInactive=false;
    private Collider2D myCollider;
    private float dieTimer = 0,stayTimer=0,myMaxHitpoints;
    private Vector2 mySpawnposition;
    public float Hitpoints { set => hitpoints = value; }
    public Player OnePlayer { get => playerM; }
    public bool IsInactive { get => isInactive; }
    #endregion



    private void Awake()
    {
        myCollider = this.GetComponent<Collider2D>();
        mySpawnposition = this.transform.position;
        myMaxHitpoints = hitpoints;
    }
    /// <summary>
    /// Should be called once per frame. Defines enemy movement.
    /// </summary>
    public virtual void Routine()
    {
        if (isKilled)
        {
            if (!myAS2.isPlaying)
            {
                Die();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the Triggercollider hits the Player, the player takes damage.
        //The appropriate Player is called to take damage. 
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
    private void OnTriggerStay2D(Collider2D collision)
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
    /// <summary>
    /// Reduce hitpoints, if nessescary die.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void TakeDamage(float damage, bool isMelee)
    {
        //If Enemy takes more damage than he has hitpoints:
        //Play the getting hit sound (appropriate for the weapon he was hit by), 
        //Play the die sound,
        //set isKilled true, so the Enemy is destroyed after deathSound stops playing and
        //disable collider.
        if (damage >= hitpoints)
        {
            hitpoints -= damage;
            if (isMelee)
            {
                myAS1.clip = getHitSoundMelee;
                myAS1.Play();
            }
            else
            {
                myAS1.clip = getHitSoundRanged;
                myAS1.Play();
            }
            myAS2.clip = dieSound;
            myAS2.Play();
            isKilled = true;
            mySpriteRenderer.enabled = false;
            myCollider.enabled = false;
        }
        //If enemy takes less damage than he has hitpoints:
        //Play the getting hit sound (appropriate for the weapon he was hit by) and
        //reduce hitpoints by damage.
        else
        {
            if (isMelee)
            {
                myAS1.clip = getHitSoundMelee;
                myAS1.Play();
            }
            else
            {
                myAS1.clip = getHitSoundRanged;
                myAS1.Play();
            }
            KnockMeBack();
            hitpoints -= damage;
        }

    }
    /// <summary>
    /// Deactivates all relevant components on this enemy.
    /// </summary>
    private void Die()
    {
        myAS1.clip = null;
        myAS2.clip = null;
        isInactive = true;
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// Reactivates all relevant components on this enemy. Resets all routines to starting point.
    /// </summary>
    public virtual void Respawn()
    {
        isKilled = false;
        isInactive = false;
        hitpoints = myMaxHitpoints;
        myCollider.enabled = true;
        mySpriteRenderer.enabled = true;
        this.gameObject.SetActive(true);
        this.transform.position = mySpawnposition;
    }
    /// <summary>
    /// Knocks Enemy away from the player.
    /// </summary>
    private void KnockMeBack()
    {
        //Knockback this enemy either to the left or right, depending on enemy position relative to player position.
        if (playerM.transform.position.x < this.transform.position.x)
        {
            myRB.AddForce(knockbackStrength);
        }
        else
        {
            myRB.AddForce(new Vector2(knockbackStrength.x * -1, knockbackStrength.y));
        }
    }
}
