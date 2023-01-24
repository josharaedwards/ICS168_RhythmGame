using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using UnityEngine;

public class BeatmapLoader : MonoBehaviour
{
    private BeatmapLoader instance;

    [SerializeField] private string beatMapName;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private GameObject endSongTriggerPrefab;
    [SerializeField] private int sortingOrder;

    private string json_dump = "";
    public static bool JSONLoaded = false;

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
        json_dump = JsonUtility.ToJson(beatmap);
        Debug.Log(json_dump);

        Directory.CreateDirectory(Application.dataPath + "/Beatmap/");
        File.WriteAllText(Application.dataPath + "/Beatmap/" + beatMapName + ".json", json_dump);

        Debug.Log("SAVED BEATMAP " + beatMapName + " at " + Application.dataPath + "/Beatmap/" + beatMapName + ".json");
    }

    IEnumerator WebRequestJSONFile()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + beatMapName + ".json");
            yield return uwr.SendWebRequest();
            Debug.Log(uwr.downloadHandler.text);
            json_dump = uwr.downloadHandler.text;
            JSONLoaded = true;
        }
        else
        {
            JSONLoaded = true;
        }
        
        
    }

    IEnumerator WaitingForJSON()
    {
        while(!JSONLoaded)
        {
            yield return new WaitForSeconds(0.1f);
        }
        LoadBeatmap();
    }

    public void Load()
    {
        JSONLoaded = false; //JSON gets turned to true within Start in BeatMapController.
        StartCoroutine(WebRequestJSONFile());
        StartCoroutine(WaitingForJSON());
    }



    public void LoadBeatmap()
    {
        // Load Beatmap object from json file

        if(Application.platform != RuntimePlatform.WebGLPlayer)
        {
            json_dump = File.ReadAllText(Application.streamingAssetsPath + "/" + beatMapName + ".json");
            Debug.Log(json_dump);
        }

        Beatmap beatmap = JsonUtility.FromJson<Beatmap>(json_dump);    
        beatmap.InitBeatmap();

        // Initialize beatmap with notes
        float receiverPos;
        foreach (Receiver receiver in beatmap.beatmap.Keys)
        {
            Debug.Log("Setting Up BeatMap");

            receiverPos = ReceiverPos.pos[receiver];
            Quaternion rotation = transform.rotation;
            if (receiver == Receiver.LEFT_UP || receiver == Receiver.RIGHT_UP)  // If lane is up then rotate the note 180deg
            {
                rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 180f);
            }

            foreach (float pos in beatmap.beatmap[receiver])
            {
                GameObject note = Instantiate(notePrefab, Vector3.zero, rotation, transform);
                note.transform.localPosition = new Vector3(receiverPos, pos, 0f);
                note.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            }
        }
        // Init EndSongTrigger
        GameObject endSongTrigger = Instantiate(endSongTriggerPrefab, Vector3.zero, transform.rotation, transform);
        endSongTrigger.transform.localPosition = new Vector3(0f, beatmap.endTriggerPos, 0f);
        endSongTrigger.SetActive(true);


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
