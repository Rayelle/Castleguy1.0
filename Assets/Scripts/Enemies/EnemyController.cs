using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Enemy[] AllEnemies;
    private bool[] EnemiesActive;
    [SerializeField]
    private float EnemyRange;

    private void Awake()
    {
        //Boolean for managing enemies. Enemies which are turned off will not do any behaviour.
        //In Prototype this is not implemented yet.
        EnemiesActive = new bool[AllEnemies.Length];
        //For now each enemy will be active. To be changed later!
        for (int i = 0; i < EnemiesActive.Length; i++)
        {
            EnemiesActive[i] = false;
        }

    }
    /// <summary>
    /// Respawns all enemies in AllEnmies array.
    /// </summary>
    public void RespawnAllEnemies()
    {
        for (int i = 0; i < AllEnemies.Length; i++)
        {
            AllEnemies[i].gameObject.SetActive(true);
            AllEnemies[i].Respawn();
            EnemiesActive[i] = false;
        }
    }
    void Update()
    {
        //Call Enemy.Routine in each Enemy which is Controlled by this EnemyController.
        for (int i = 0; i < AllEnemies.Length; i++)
        {
            if (EnemiesActive[i])
            {
                if (!AllEnemies[i].IsInactive)
                {
                    AllEnemies[i].Routine();
                }
            }
            else
            {
                //check if player is within enemy range, if yes they are activated.
                if (!AllEnemies[i].IsInactive && new Vector2(AllEnemies[i].OnePlayer.transform.position.x - AllEnemies[i].transform.position.x, AllEnemies[i].OnePlayer.transform.position.y - AllEnemies[i].transform.position.y).magnitude <= EnemyRange)
                {
                    EnemiesActive[i] = true;
                }
            }
        }
    }
}
