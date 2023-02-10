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
        LevelLoaderScript.instance.LoadNextScene("SinglePlayerMultiPlayer");
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }

    public void SinglePlayerButton()
    {
        // Single Player
        GameManager.instance.SetGameState(GameManager.GameStates.SinglePlayer);

        LevelLoaderScript.instance.LoadNextScene("SongSelect");
    }

    public void LocalMultiPlayerButton()
    {
        //Local Multi Player
        GameManager.instance.SetGameState(GameManager.GameStates.LocalMultiplayer);

        LevelLoaderScript.instance.LoadNextScene("SongSelect");
    }

    public void OnlineMultiPlayerButton()
    {
        //Online Multi Player
        GameManager.instance.SetGameState(GameManager.GameStates.OnlineMultiplayer);

        LevelLoaderScript.instance.LoadNextScene("SongSelect");
    }

    public void MainMenuButton()
    {
        LevelLoaderScript.instance.LoadNextScene("TitleScr");
    }
}