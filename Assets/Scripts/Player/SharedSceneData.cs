using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedSceneData : MonoBehaviour
{
    [Header("Prefab Data")]
    [SerializeField]
    private Rigidbody2D myRB;
    [SerializeField]
    private ParticleSystem dust;
    [SerializeField]
    private PlayerController myPC;
    [SerializeField]
    Checkpoint checkpointPrefab;
    [SerializeField]
    Health myHealthScript;
    [SerializeField]
    private AudioClip[] getHitSounds, jumpSounds;
    [SerializeField]
    private AudioClip landingSound, deathSound;
    [Header("Scene Data")]
    [SerializeField]
    private AmmoUIScript myAmmoUI;
    [SerializeField]
    private PotionUI myPotionUI;
    [SerializeField]
    private Camera2DScript myCamera2DScript;
    [SerializeField]
    private EnemyController mainEnemyController;
    [SerializeField]
    private GameObject myDeathText;


    public AudioClip LandingSound { get => landingSound; }
    public AudioClip DeathSound { get => deathSound; }
    public AudioClip[] GetHitSounds { get => getHitSounds; }
    public AudioClip[] JumpSounds { get => jumpSounds; }
    public Rigidbody2D MyRB { get => myRB; set => myRB = value; }
    public ParticleSystem Dust { get => dust; set => dust = value; }
    public PlayerController MyPC { get => myPC; set => myPC = value; }
    public AmmoUIScript MyAmmoUI { get => myAmmoUI; set => myAmmoUI = value; }
    public PotionUI MyPotionUI { get => myPotionUI; set => myPotionUI = value; }
    public Checkpoint CheckpointPrefab { get => checkpointPrefab; set => checkpointPrefab = value; }
    public Health MyHealthScript { get => myHealthScript; set => myHealthScript = value; }
    public Camera2DScript MyCamera2DScript { get => myCamera2DScript; set => myCamera2DScript = value; }
    public EnemyController MainEnemyController { get => mainEnemyController; set => mainEnemyController = value; }
    public GameObject MyDeathText { get => myDeathText; set => myDeathText = value; }
}
