using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapResetter : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 5.0f)] private float resetSpeed = 1;

    private Transform[] beatPositions;
    // Start is called before the first frame update
    void Start()
    {
        beatPositions = GetComponentsInChildren<Transform>();
        for (int i = 1; i < beatPositions.Length; i++)
        {
            //Debug.Log("changed");
            beatPositions[i].localPosition = Vector3.Scale(beatPositions[i].localPosition, new Vector3(1.0f, 1/resetSpeed, 1.0f));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
