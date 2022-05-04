using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField]
    SharedSceneData mySceneData;

    [SerializeField]
    AudioSource myAS;

    [SerializeField]
    Weapon myWeapon;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    float wallJumpForceX, wallJumpForceY;

    [SerializeField]
    GameObject myAnimationParent;

    [SerializeField]
    Vector2 myKnockback;

    [SerializeField]
    private float moveAcceleration, moveMaxSpeed, moveDeceleration, jumpForce, dashForce, timeUntilAttackHits, timeAfterAttackHits, dashCooldown = 0.5f, attackCooldown = 0.8f, invincibilityDuration, deathAnimationCD = 1.0f;
    private float attackTimer = 0.0f, dashTimer = 0.0f, myGravityScale, getHitAnimationLength = 0.3293596f, invincibilityTimer = 0.0f, deathAnimationTimer;
    private bool onWall, airborne, dashActive = false, lookingRight = true, isInvincible = false, attacking = false, attackingHit = false, dieAnimationActive = false;




    public bool OnWall { get => onWall; set => onWall = value; }
    public bool Airborne { get => airborne; set => airborne = value; }
    public bool DashActive { get => dashActive; set => dashActive = value; }
    public float DashTimer { get => dashTimer; set => dashTimer = value; }
    public float DashCooldown { get => dashCooldown; }
    public bool LookingRight { get => lookingRight; }
    public Animator MyAnimator { get => myAnimator; }
    public float GetHitAnimationLength { get => getHitAnimationLength; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public float AttackCooldown { get => attackCooldown; }
    public bool Attacking { get => attacking; set => attacking = value; }
    public float TimeUntilAttackHits { get => timeUntilAttackHits; }
    public float TimeAfterAttackHits { get => timeAfterAttackHits; }
    public bool AttackingHit { get => attackingHit; set => attackingHit = value; }
    public float InvincibilityDuration { get => invincibilityDuration; }
    public float InvincibilityTimer { get => invincibilityTimer; set => invincibilityTimer = value; }
    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }
    public float DeathAnimationTimer { get => deathAnimationTimer; set => deathAnimationTimer = value; }
    public float DeathAnimationCD { get => deathAnimationCD; }
    public bool OnWall1 { get => onWall; set => onWall = value; }
    public bool Airborne1 { get => airborne; set => airborne = value; }
    public bool DashActive1 { get => dashActive; set => dashActive = value; }
    public bool LookingRight1 { get => lookingRight; set => lookingRight = value; }
    public bool IsInvincible1 { get => isInvincible; set => isInvincible = value; }
    public bool Attacking1 { get => attacking; set => attacking = value; }
    public bool AttackingHit1 { get => attackingHit; set => attackingHit = value; }
    public bool DieAnimationActive { get => dieAnimationActive; set => dieAnimationActive = value; }
    #endregion
    private void Awake()
    {
        //At the start of each Scene, creates a Checkpoint at players location.
        PlayerData.MyCheckpoint = Checkpoint.Instantiate(mySceneData.CheckpointPrefab, this.transform.position, this.transform.rotation);
        PlayerData.MyCheckpoint.SpawnPosition = this.transform.position;

        mySceneData.MyHealthScript.UpdateHealthPoints();
        mySceneData.MyAmmoUI.UpdateAmmo();
    }

    /// <summary>
    /// Moves player left/right depending on xInput
    /// </summary>
    /// <param name="xInput">1 to move right, -1 to move left, 0 to break</param>
    public void Move(float xInput)
    {
        //Depending on xInput moves the player in three different ways
        switch (xInput)
        {
            //brakes the player
            case 0:
                if (mySceneData.MyRB.velocity.x > 0)
                {
                    mySceneData.MyRB.AddForce(Vector2.left * moveDeceleration * Time.deltaTime);
                    //when velocity is lower than 1 player is stopped
                    if (mySceneData.MyRB.velocity.x < 1)
                    {
                        mySceneData.MyRB.velocity = new Vector2(0, mySceneData.MyRB.velocity.y);
                    }
                }
                if (mySceneData.MyRB.velocity.x < 0)
                {
                    mySceneData.MyRB.AddForce(Vector2.right * moveDeceleration * Time.deltaTime);
                    //when velocity is higher than -1 player is stopped
                    if (mySceneData.MyRB.velocity.x > -1)
                    {
                        mySceneData.MyRB.velocity = new Vector2(0, mySceneData.MyRB.velocity.y);
                    }
                }
                //disable running animation
                myAnimator.SetBool("IsRunning", false);
                break;
            //moves player left
            case -1:
                if (mySceneData.MyRB.velocity.x > moveMaxSpeed * -1)
                {
                    //if player isn't airborne Running-animation is displayed
                    if (!airborne)
                    {
                        myAnimator.SetBool("IsRunning", true);
                    }
                    mySceneData.MyRB.AddForce(Vector2.left * moveAcceleration * Time.deltaTime);
                    //flip facing and keep track of facing in bool lookingRight
                    lookingRight = false;
                    myAnimationParent.transform.localScale = new Vector3(-1, 1, 1);
                }
                break;
            //moves player right
            case 1:

                if (mySceneData.MyRB.velocity.x < moveMaxSpeed)
                {
                    //if player isn't airborne Running-animation is displayed
                    if (!airborne)
                    {
                        myAnimator.SetBool("IsRunning", true);
                    }
                    mySceneData.MyRB.AddForce(Vector2.right * moveAcceleration * Time.deltaTime);
                    //flip facing and keep track of facing in bool lookingRight
                    lookingRight = true;
                    myAnimationParent.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
        }


    }
    /// <summary>
    /// Player jumps when on the ground. Alternatively, player walljumps when on a wall while not on the ground.
    /// </summary>
    public void Jump()
    {
        //Only jump if player touches the ground
        if (!airborne)
        {
            //play jump sound, add jumping force and display jumping animation

            myAS.clip = mySceneData.JumpSounds[Random.Range((int)0, mySceneData.JumpSounds.Length)];
            myAS.Play();
            mySceneData.MyRB.AddForce(Vector2.up * jumpForce);

            myAnimator.SetBool("IsJumping", true);
        }
        else
        {
            //Player can walljump while touching a wall
            if (OnWall)
            {
                //Velocity is reset before walljump to make walljump more recognizable
                mySceneData.MyRB.velocity = Vector2.zero;

                //walljump adds force up and to the back
                if (lookingRight)
                {
                    mySceneData.MyRB.AddForce(Vector2.left * wallJumpForceX);
                    mySceneData.MyRB.AddForce(Vector2.up * wallJumpForceY);
                }
                else
                {
                    mySceneData.MyRB.AddForce(Vector2.right * wallJumpForceX);
                    mySceneData.MyRB.AddForce(Vector2.up * wallJumpForceY);
                }
                //also play jumpSound

                myAS.clip = mySceneData.JumpSounds[Random.Range((int)0,mySceneData.JumpSounds.Length)];
                myAS.Play();
            }
        }
    }
    /// <summary>
    /// Should be called once per frame while dash is active.
    /// </summary>
    public void DashRunning()
    {
        //after 0.3 seconds dash is turned off, GravityScale is reset and playerVelocity is set to 0.
        if (dashTimer > 0.3)
        {
            dashActive = false;
            mySceneData.MyRB.gravityScale = myGravityScale;
            mySceneData.MyRB.velocity = Vector2.zero;

        }
    }
    /// <summary>
    /// Should be called once to start the dash behaviour.
    /// </summary>
    public void Dash()
    {
        //Gravity is set to 0, Velocity is set to 0,
        //timer is started by setting dashActive.
        myGravityScale = mySceneData.MyRB.gravityScale;
        mySceneData.MyRB.velocity = Vector2.zero;
        dashActive = true;
        if (!lookingRight)
        {
            mySceneData.Dust.gameObject.transform.localScale = new Vector3(-1,1,1);
            mySceneData.Dust.Play();
        }
        else
        {
            mySceneData.Dust.gameObject.transform.localScale = new Vector3(1, 1, 1);
            mySceneData.Dust.Play();
        }
        mySceneData.MyRB.gravityScale = 0;
        dashTimer = 0;
        //dashForce is added in the appropriate direction.
        if (lookingRight)
        {
            mySceneData.MyRB.AddForce(Vector2.right * dashForce);
        }
        else
        {
            mySceneData.MyRB.AddForce(Vector2.left * dashForce);
        }

    }
    /// <summary>
    /// Should be called once to start attack behaviour.
    /// </summary>
    public void AttackStart()
    {
        //Attack is very different depending on active player.
        if (PlayerData.PlayerIsMelee)
        {
            //player cannot move while attack 
            mySceneData.MyPC.PlayerHasControl = false;
            attacking = true;
            //if player is in the air, JumpAttack is activated in animator, this is not implemented yet.

            //stop running animation if nessescary, set attacking animation. 
            myAnimator.SetBool("IsRunning", false);
            myAnimator.SetBool("IsAttacking", true);
            
            //set timer to track attackstate
            attackTimer = 0;
            attackingHit = false;

        }

        else
        {
            //ranged player can only attack if he has ammo
            if (PlayerData.Ammo > 0)
            {
                //player cannot move while attack 
                mySceneData.MyPC.PlayerHasControl = false;
                attacking = true;
                //if player is in the air, JumpAttack is activated in animator, this is not implemented yet.

                //stop running animation if nessescary, set attacking animation. 
                myAnimator.SetBool("IsRunning", false);
                myAnimator.SetBool("IsAttacking", true);
                
                //set timer to track attackstate and reduce player ammo
                attackTimer = 0;
                attackingHit = false;
                PlayerData.Ammo -= 1;
                mySceneData.MyAmmoUI.UpdateAmmo();

            }
        }

    }
    /// <summary>
    /// Calls attack funtion in myWeapon.
    /// </summary>
    /// <param name="lookingRight"></param>
    public void Attack(bool lookingRight)
    {
        myWeapon.Attack(lookingRight);
    }

    /// <summary>
    /// To be called to end attack behaviour
    /// </summary>
    public void AttackEnd()
    {
        //cancel attaking animation, grant player control again.
        myAnimator.SetBool("IsAttacking", false);
        attacking = false;
        mySceneData.MyPC.PlayerHasControl = true;
    }
    /// <summary>
    /// Reduce playerhealth by given amount, die if appopriate.
    /// </summary>
    /// <param name="damage">damage to be applied</param>
    public void PlayerTakeDamage(float damage)
    {
        //player cannot take any damage if he is invincible after being hit.
        if (isInvincible)
        {
            return;
        }
        //if damage exceeds players remaining health, player dies
        if (damage >= PlayerData.Healthpoints)
        {
            //plays deathsound, displays hitpoints as 0, calls PlayerDie()
            if (!dieAnimationActive)
            {
            myAS.clip = mySceneData.DeathSound;
            myAS.Play();
            }
            PlayerData.Healthpoints = 0;
            mySceneData.MyHealthScript.UpdateHealthPoints();
            if (!dieAnimationActive)
            PlayerDie();
        }
        else
        {
            //apply knockback in appopriate direction
            if (lookingRight)
            {
                mySceneData.MyRB.velocity = Vector2.zero;
                mySceneData.MyRB.AddForce(myKnockback);
            }
            else
            {
                mySceneData.MyRB.velocity = Vector2.zero;
                mySceneData.MyRB.AddForce(new Vector2(myKnockback.x * -1, myKnockback.y));
            }
            //play getHitSound, play get-hit-animation, reduce health, display reduced health and activate player invincibility.

            //Choose a random getHitSound from SharedData
            myAS.clip = mySceneData.GetHitSounds[Random.Range((int)0, mySceneData.GetHitSounds.Length)];

            myAS.Play();
            myAnimator.SetBool("IsGettingHit", true);
            PlayerData.GetHitTimer = 0.0f;
            PlayerData.Healthpoints -= damage;
            mySceneData.MyHealthScript.UpdateHealthPoints();
            isInvincible = true;
            invincibilityTimer = 0;
        }
    }
    /// <summary>
    /// increases hitpoint by given amount
    /// </summary>
    /// <param name="healAmout">amount to increase hitpoints by</param>
    public void PlayerHeal(float healAmout)
    {
        PlayerData.Healthpoints += healAmout;
        mySceneData.MyHealthScript.UpdateHealthPoints();
    }
    /// <summary>
    /// starts dying behaviour
    /// </summary>
    private void PlayerDie()
    {
        //set dyingAnimation, start timer until time is stopped, reset player health and ammo and take away playerControl.
        myAnimator.SetBool("IsDead", true);
        myAnimator.SetTrigger("IsDying");
        dieAnimationActive = true;
        mySceneData.MyPC.PlayerHasControl = false;
        deathAnimationTimer = 0;
        mySceneData.MyDeathText.SetActive(true);




    }
    /// <summary>
    /// Returns player to life at last checkpoint
    /// </summary>
    public void PlayerRespawn()
    {
        //Only works if player as dead and therefore time is paused
        if (Time.timeScale == 0)
        {
            //resets position to last chepoint, resets timeScale to 1, stops player-dying-animation,
            //resets camera to player position, displays refilled hitpointsand give player controls.
            Time.timeScale = 1;
            mySceneData.MyRB.transform.position = PlayerData.MyCheckpoint.SpawnPosition;
            myAnimator.SetBool("IsDead", false);
            dieAnimationActive = false;
            mySceneData.MyPC.PlayerHasControl = true;
            PlayerData.Healthpoints = PlayerData.MaxHealthPoints;
            PlayerData.Ammo = 5;
            mySceneData.MyHealthScript.UpdateHealthPoints();
            mySceneData.MyAmmoUI.UpdateAmmo();
            mySceneData.MyCamera2DScript.SearchforPlayer();
            mySceneData.MainEnemyController.RespawnAllEnemies();
            mySceneData.MyDeathText.SetActive(false);
        }

    }
    /// <summary>
    /// Consume healthpotion if availabe
    /// </summary>
    public void Consume()
    {
        if (PlayerData.HealthPotion && PlayerData.Healthpoints != 10 && PlayerData.Healthpoints != 0)
        {
            //refill health and remove healthpotion.
            PlayerData.Healthpoints = 10;
            PlayerData.HealthPotion = false;
            mySceneData.MyPotionUI.UpdatePotion();
            mySceneData.MyHealthScript.UpdateHealthPoints();
        }

    }
}
