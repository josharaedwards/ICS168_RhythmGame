using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public enum Receiver
{
    LEFT_UP,
    LEFT_DOWN,
    RIGHT_UP,
    RIGHT_DOWN
}

public static class ReceiverPos
{
    public const float LEFT_UP = -1.5f;
    public const float LEFT_DOWN = -0.5f;
    public const float RIGHT_DOWN = 0.5f;
    public const float RIGHT_UP = 1.5f;

    public static readonly Dictionary<Receiver, float> pos = new Dictionary<Receiver, float>
    {
        [Receiver.LEFT_UP] = LEFT_UP,
        [Receiver.LEFT_DOWN] = LEFT_DOWN,
        [Receiver.RIGHT_DOWN] = RIGHT_DOWN,
        [Receiver.RIGHT_UP] = RIGHT_UP
    };
}

[Serializable]
public class Beatmap
{
    // Has to do this since Unity JSON Serializer does not serialize array
    public List<float> left_up;
    public List<float> left_down;
    public List<float> right_down;
    public List<float> right_up;

    public Dictionary<Receiver, List<float>> beatmap;

    public Beatmap(GameObject beatMap)
    {
        InitBeatmap();
        Transform note;
        for (int i = 0; i < beatMap.transform.childCount; i++)
        {
            note = beatMap.transform.GetChild(i);
            switch (note.localPosition.x)
            {
                case ReceiverPos.LEFT_UP:
                    left_up.Add(note.localPosition.y);
                    break;
                case ReceiverPos.LEFT_DOWN:
                    left_down.Add(note.localPosition.y);
                    break;
                case ReceiverPos.RIGHT_DOWN:
                    right_down.Add(note.localPosition.y);
                    break;
                case ReceiverPos.RIGHT_UP:
                    right_up.Add(note.localPosition.y);
                    break;
            }
        }
    }


    public void InitBeatmap()
    {
        if (left_up == null)
            left_up = new List<float>();
        if (left_down == null)
            left_down = new List<float>();
        if (right_down == null)
            right_down = new List<float>();
        if (right_up == null)
            right_up = new List<float>();

        beatmap = new Dictionary<Receiver, List<float>>
        {
            [Receiver.LEFT_UP] = left_up,
            [Receiver.LEFT_DOWN] = left_down,
            [Receiver.RIGHT_DOWN] = right_down,
            [Receiver.RIGHT_UP] = right_up
        };
    }


}
