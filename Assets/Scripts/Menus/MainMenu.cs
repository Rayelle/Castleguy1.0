using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    SpeedrunTimer mySpeedrunTimer;
    
    //Game start method
    public void StartGame()
    {
        PlayerData.PlayerIsMelee = true;
        PlayerData.Healthpoints = PlayerData.MaxHealthPoints;
        PlayerData.Ammo = 10;
        //still have to write the right SceneName/Number
        if (PauseMenu.GameIsPaused)
        {
            //Sets Timer to zero
            mySpeedrunTimer.Time = 0;

            //checks if Speedrun Mode is activated to know which scene to load
            if (PlayerData.SpeedrunOn)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
            SceneManager.LoadScene(1);
            }

        }
        else
        {
            //Sets Timer to zero
            mySpeedrunTimer.Time = 0;

            //checks if Speedrun Mode is activated to know which scene to load
            if (PlayerData.SpeedrunOn)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }

    }

    //Game quitting method
    public void QuitGame()
    {
        Application.Quit();
    }

    //Going back to the start menu method
    public void BackToMenu()
    {
        //still have to write the right SceneName/Number
        SceneManager.LoadScene(0);
    }

    //Turns the speedrunMode on/off 
    public void changeSpeedrunMode()
    {
        if (PlayerData.SpeedrunOn)
        {
            PlayerData.SpeedrunOn = false;
        }
        else
        {
            PlayerData.SpeedrunOn = true;
        }
    }

}
