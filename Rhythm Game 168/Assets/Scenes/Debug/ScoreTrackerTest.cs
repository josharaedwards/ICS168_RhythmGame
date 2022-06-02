using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrackerTest : MonoBehaviour
{
    
    public int meter = 3;

    public int wiggled = 0;

    public int superb = 0;
    public int good = 0;
    // public int almost = 0;
    public int bad = 0;
    public int gloomy = 0;
    public int miss = 0;

    public static ScoreTrackerTest instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
