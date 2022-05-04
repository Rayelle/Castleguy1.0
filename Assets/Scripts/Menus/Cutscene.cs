using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{   [SerializeField]
    private VideoPlayer videoPlayer;
 
    void Start()
    {
     
        videoPlayer.loopPointReached += LoadScene;   
    }

    //Loads next Scene after Cutscene Video ends
    void LoadScene(VideoPlayer vp)
    {
        videoPlayer.Stop();
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(2);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Cutscene"));
    }


    public void NextScene()
    {
        videoPlayer.Stop();
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadSceneAsync(2);
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Cutscene"));
    }
}
