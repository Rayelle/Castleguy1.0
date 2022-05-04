using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalMenu : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerData.Healthpoints = 10;
            PlayerData.Ammo = 10;
            PlayerData.PlayerIsMelee = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
