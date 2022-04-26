using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;
    public int miss = 0;

    public static ScoreTracker instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
