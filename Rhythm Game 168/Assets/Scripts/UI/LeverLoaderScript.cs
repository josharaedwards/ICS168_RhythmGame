using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverLoaderScript : MonoBehaviour
{

    public static LeverLoaderScript instance;
    public Animator transition;

    public float transitionTime = 1.0f;

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
        transition.SetTrigger("Win");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log(scene + "Loaded!");
        SceneManager.LoadScene(scene);
    }
}
