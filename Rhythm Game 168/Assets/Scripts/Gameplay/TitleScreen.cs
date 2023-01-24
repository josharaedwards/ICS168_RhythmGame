using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"Final Title.mp4"); 
            videoPlayer.Play();
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
