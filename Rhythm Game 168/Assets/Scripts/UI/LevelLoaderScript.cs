using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    public static LevelLoaderScript instance;
    public Animator transition;

    public float transitionTime = 1.0f;

    public float deathAnimTime = 1f;

    public float winAnimTime = 1.5f;

    void Start()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void LoadNextScene(string scene)
    {
        StartCoroutine(LoadScene(scene));
    }

    private IEnumerator LoadScene(string scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log(scene + "Loaded!");
        SceneManager.LoadScene(scene);
    }
    public void LoadNextSceneFromDead(string scene)
    {
        StartCoroutine(LoadSceneFromDead(scene));
    }

    private IEnumerator LoadSceneFromDead(string scene)
    {
        yield return new WaitForSeconds(deathAnimTime);
        transition.SetTrigger("Lose");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log(scene + "Loaded!");
        SceneManager.LoadScene(scene);
    }

    public void LoadNextSceneFromWin(string scene)
    {
        StartCoroutine(LoadSceneFromWin(scene));
    }

    private IEnumerator LoadSceneFromWin(string scene)
    {
        yield return new WaitForSeconds(winAnimTime);

        transition.SetTrigger("Win");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log(scene + "Loaded!");
        SceneManager.LoadScene(scene);
    }

    public void LeaveRoom(string scene)
    {
        StartCoroutine(LeaveRoomTransition(scene));
    }

    private IEnumerator LeaveRoomTransition(string scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        GameManager.instance.LeaveRoom();
    }
}
