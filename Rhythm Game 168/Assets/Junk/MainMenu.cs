using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        UnityEngine.SceneManagement.SceneManager.LoadScene("SinglePlayerMultiPlayer");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }

    public void SinglePlayerButton()
    {
        // Single Player
        UnityEngine.SceneManagement.SceneManager.LoadScene("SongSelect");
    }

    public void MultiPlayerButton()
    {
        // Multi Player
        UnityEngine.SceneManagement.SceneManager.LoadScene("Multi");
    }
}