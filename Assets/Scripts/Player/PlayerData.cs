using UnityEngine;

public static class PlayerData
{
    private static float healthpoints = 10;
    private static float wallJumpCooldown = 0.2f, maxHealthPoints = 10, invincibilityTime = 2, getHitTimer = 0.0f;
    private static Vector2 wallJumpForce;
    private static Checkpoint myCheckpoint;
    private static bool playerIsMelee = true;
    private static bool healthPotion = true;             //property+
    //Checkpoint myCheckpoint;            //Property to be added
    private static uint ammo = 10;
    private static bool speedrunOn = false;
    public static float Healthpoints
    {
        get => healthpoints;
        set
        {
            healthpoints = value;
            if (maxHealthPoints < healthpoints)
            {
                healthpoints = maxHealthPoints;
            }
        }
    }
    public static float MaxHealthPoints { get => maxHealthPoints; }
    public static float WallJumpCooldown { get => wallJumpCooldown; set => wallJumpCooldown = value; }
    public static uint Ammo { get => ammo; set => ammo = value; }
    public static Checkpoint MyCheckpoint { get => myCheckpoint; set => myCheckpoint = value; }
    public static float GetHitTimer { get => getHitTimer; set => getHitTimer = value; }
    public static bool PlayerIsMelee { get => playerIsMelee; set => playerIsMelee = value; }
    public static bool HealthPotion { get => healthPotion; set => healthPotion = value; }
    public static bool SpeedrunOn { get => speedrunOn; set => speedrunOn = value; }
}
