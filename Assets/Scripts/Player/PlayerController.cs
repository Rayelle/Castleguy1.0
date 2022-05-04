using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private float horizontalInput, switchAnimationTimer = 0, switchAnimationLength = 0.8f;
    private bool jumpButtonPressed, dashButtonPressed, consumableButtonPressed, switchButtonPressed, menuButtonPressed, attackButtonPressed, playerHasControl = true, isMelee = true, isSwitching = false;

    public bool PlayerHasControl { get => playerHasControl; set => playerHasControl = value; }
    [SerializeField]
    private Player controlledPlayer, alternativePlayer;
    [SerializeField]
    GameObject FlipMelee, FlipRanged, myPlayerSwitchAnimation;
    [SerializeField]
    SpeedrunTimer mySpeedrunTimer;
    [SerializeField]
    PauseMenu myPause;
    [SerializeField]
    TextMeshProUGUI myTimerText;
    #endregion


    private void Update()
    {
        mySpeedrunTimer.Time += Time.deltaTime;
        if (PlayerData.SpeedrunOn)
        {
            myTimerText.text = ("time: " + Math.Round(mySpeedrunTimer.Time,2));
        }
        //PlayerSwitch Timer
        if (isSwitching == true)
        {
            switchAnimationTimer += Time.deltaTime;

            if (switchAnimationTimer >= switchAnimationLength)
            {
                myPlayerSwitchAnimation.SetActive(false);
                isSwitching = false;
                switchAnimationTimer = 0;
            }
        }
        //increment all timers
        controlledPlayer.DashTimer += Time.deltaTime;
        controlledPlayer.AttackTimer += Time.deltaTime;
        controlledPlayer.DeathAnimationTimer += Time.deltaTime;
        if (controlledPlayer.DieAnimationActive == true)
        {
            //after death animation is played, stop gametime and reset players animator.
            if (controlledPlayer.DeathAnimationTimer > controlledPlayer.DeathAnimationCD)
            {
                Time.timeScale = 0;
                controlledPlayer.DieAnimationActive = false;
            }
        }


        PlayerData.GetHitTimer += Time.deltaTime;
        //After being hit player is invincible for a short duration. 
        if (controlledPlayer.IsInvincible)
        {
            controlledPlayer.InvincibilityTimer += Time.deltaTime;
            if (controlledPlayer.InvincibilityTimer >= controlledPlayer.InvincibilityDuration)
            {
                //After invincibility-duration player can take damage again.
                controlledPlayer.IsInvincible = false;
            }
        }

        if (PlayerData.GetHitTimer >= controlledPlayer.GetHitAnimationLength)
        {
            //after interval is reached, turn off GettingHit animation.
            controlledPlayer.MyAnimator.SetBool("IsGettingHit", false);
        }

        if (controlledPlayer.Attacking == true)
        {
            if (!controlledPlayer.AttackingHit && controlledPlayer.AttackTimer > controlledPlayer.TimeUntilAttackHits)
            {
                //after certain inverval, call Attack() function
                controlledPlayer.Attack(controlledPlayer.LookingRight);
                controlledPlayer.AttackingHit = true;
            }

            if (controlledPlayer.AttackTimer > controlledPlayer.TimeAfterAttackHits)
            {
                //after certain interval, call AttackEnd() function
                controlledPlayer.AttackEnd();
            }
        }
        //Detect player input and if player has control, apply those inputs.
        DetectInput();
        //Menu function can be called even when player does not have control
        //Changes PlayerHasControl and opens pausemenu
        if (menuButtonPressed)
        {
            myPause.callPause();
        }
        if (playerHasControl) ApplyInput();


    }
    /// <summary>
    /// For each Input button, set appropriate bool
    /// </summary>
    private void DetectInput()
    {
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1;

        if (Input.GetKey(KeyCode.D)) horizontalInput = 1;
        //If user does not hold left or right or if user holds left and right:
        //reduce player-speed by assigning horizontalInput to 0
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            horizontalInput = 0;

        if (Input.GetKeyDown(KeyCode.Space)) jumpButtonPressed = true;
        else jumpButtonPressed = false;

        if (Input.GetKeyDown(KeyCode.LeftShift)) dashButtonPressed = true;
        else dashButtonPressed = false;

        if (Input.GetKeyDown(KeyCode.Q)) consumableButtonPressed = true;
        else consumableButtonPressed = false;

        if (Input.GetKeyDown(KeyCode.LeftControl)) switchButtonPressed = true;
        else switchButtonPressed = false;

        if (Input.GetKeyDown(KeyCode.P)) menuButtonPressed = true;
        else menuButtonPressed = false;

        if (Input.GetKeyDown(KeyCode.Mouse0)) attackButtonPressed = true;
        else attackButtonPressed = false;

        //if player is dead and player presses R button, PlayerRespawn() is called.
        if (Input.GetKeyDown(KeyCode.R) && controlledPlayer.MyAnimator.GetBool("IsDead")) controlledPlayer.PlayerRespawn();


    }
    /// <summary>
    /// Applies input from DetectInput() and calls appropriate functions
    /// </summary>
    private void ApplyInput()
    {
        //if dash is already active, call DashRunning()
        if (controlledPlayer.DashActive)
        {
            controlledPlayer.DashRunning();
        }
        //while dash is active Other inputs are ignored
        else
        {
            //for each input, call appropriate function in active player (controlledPlayer).
            controlledPlayer.Move(horizontalInput);

            if (jumpButtonPressed) controlledPlayer.Jump();


            if (dashButtonPressed && controlledPlayer.DashTimer > controlledPlayer.DashCooldown)
                controlledPlayer.Dash();

            //SwitchCharacter is the only function managed by PlayerController
            if (switchButtonPressed) SwitchCharacter();

            if (consumableButtonPressed) controlledPlayer.Consume();


            if (attackButtonPressed && controlledPlayer.AttackTimer > controlledPlayer.AttackCooldown) controlledPlayer.AttackStart();





        }



        //Prototype dosent use this from PlayerController, will be added here later
        //if (menuButtonPressed) controlledPlayer.OpenMenu();

    }


    public void SwitchCharacter()
    {
        //Characterswitch only works while player is grounded
        if (!controlledPlayer.Airborne)
        {
            //switch out player class
            Player tmp = controlledPlayer;
            controlledPlayer = alternativePlayer;
            alternativePlayer = tmp;
            myPlayerSwitchAnimation.SetActive(true);
            isSwitching = true;


            if (isMelee)
            {
                //switch gameObject containing animations
                FlipRanged.SetActive(true);
                FlipMelee.SetActive(false);
                isMelee = false;
            }
            else
            {
                //switch gameObject containing animations
                FlipMelee.SetActive(true);
                FlipRanged.SetActive(false);
                isMelee = true;
            }
            //PlayerData bool is also switched to inform enemies, this is redunand with bool 'isMelee', will be changed after Prototype.
            if (PlayerData.PlayerIsMelee)
            {
                PlayerData.PlayerIsMelee = false;
            }
            else
            {
                PlayerData.PlayerIsMelee = true;
            }
            // Verzögerung beim Wechsel hinzufügen

        }

    }

    /// <summary>
    /// Sets wall status
    /// </summary>
    /// <param name="value"></param>
    public void setOnWall(bool value)
    {
        controlledPlayer.OnWall = value;
    }
    /// <summary>
    /// Sets airborne status
    /// </summary>
    /// <param name="value"></param>
    public void setAirborne(bool value)
    {
        controlledPlayer.Airborne = value;
    }



}
