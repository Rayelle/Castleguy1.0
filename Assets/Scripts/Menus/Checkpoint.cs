using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector2 spawnPosition;

    [SerializeField]
    private Transform respawnPosition;

    public Vector2 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }

    private void Awake()
    {
        //setting the SpawnPosition of Checkpoint
        SpawnPosition = new Vector2(respawnPosition.position.x, respawnPosition.position.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If Player hits Checkpoint his own Checkpoint is set to this one

        if (col.gameObject.layer == 8)
        {
            PlayerData.MyCheckpoint = this;
        }
    }

}
