using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour
{
    [SerializeField] private string beatMapName;
    [SerializeField] private GameObject notePrefab;

    void Start()
    {

    }


    public void setBeatmapName(string name)
    {
        beatMapName = name;
    }

    public void Save()
    {
        Beatmap beatmap = new Beatmap(gameObject);
        string json_dump = JsonUtility.ToJson(beatmap);
        Debug.Log(json_dump);

        Directory.CreateDirectory(Application.dataPath + "/Beatmap/");
        File.WriteAllText(Application.dataPath + "/Beatmap/" + beatMapName + ".json", json_dump);

        Debug.Log("SAVED BEATMAP " + beatMapName + " at " + Application.dataPath + "/Beatmap/" + beatMapName + ".json");
    }

    public void Load()
    {
        string json_dump = File.ReadAllText(Application.dataPath + "/Beatmap/" + beatMapName + ".json");
        Debug.Log(json_dump);
        Beatmap beatmap = JsonUtility.FromJson<Beatmap>(json_dump);
        beatmap.InitBeatmap();

        float receiverPos;
        foreach (Receiver receiver in beatmap.beatmap.Keys)
        {
            receiverPos = ReceiverPos.pos[receiver];
            Quaternion rotation = transform.rotation;
            if (receiver == Receiver.LEFT_UP || receiver == Receiver.RIGHT_UP)
            {
                rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 180f);
            }

            foreach (float pos in beatmap.beatmap[receiver])
            {
                GameObject note = Instantiate(notePrefab, Vector3.zero, rotation, transform);
                note.transform.localPosition = new Vector3(receiverPos, pos, 0f);
            }
        }
        Debug.Log("LOADED BEATMAP " + beatMapName + " from " + Application.dataPath + "/Beatmap/" + beatMapName + ".json");
    }

    public void ClearEditor()
    {
        List<Transform> notes = new List<Transform>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            notes.Add(gameObject.transform.GetChild(i));
        }

        foreach (Transform note in notes)
        {
            DestroyImmediate(note.gameObject);
        }
    }

    public void Clear()
    {
        List<Transform> notes = new List<Transform>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            notes.Add(gameObject.transform.GetChild(i));
        }

        foreach (Transform note in notes)
        {
            Destroy(note.gameObject);
        }
    }


}
