using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BeatmapControllerTest : MonoBehaviour
{
    [SerializeField] private bool started;

    [SerializeField] private int beatPerMinute;

    [SerializeField] private Controls m_Controls;
    private float beatPerSecond;


    // Start is called before the first frame update
    void Awake()
    {

        beatPerSecond = beatPerMinute / 60f;

    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            transform.position -= new Vector3(0f, beatPerSecond * Time.deltaTime, 0f);
        }
    }
}
