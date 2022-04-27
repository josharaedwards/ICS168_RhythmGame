using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeatmapController : MonoBehaviour
{
    [SerializeField] private bool started = false;

    [SerializeField] private int beatPerMinute;
    private float beatPerSecond;


    // Start is called before the first frame update
    void Awake()
    {
        beatPerSecond = beatPerMinute / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.anyKeyDown)
            {
                started = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, beatPerSecond * Time.deltaTime, 0f);
        }
    }
}
