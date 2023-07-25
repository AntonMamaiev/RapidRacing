using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameState gameState;
    public void PlayTrack1()
    {
        SceneManager.LoadScene("Track1");
    }
    public void PlayTrack2()
    {
        SceneManager.LoadScene("Track2");
    }
    public void PlayTrack3()
    {
        SceneManager.LoadScene("Track3");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MENU");
    }
    public void Restart()
    {
        gameState.RaceRestarted = true;
    }
    public void NextTrack()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index == 4)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(index);


    }

}
