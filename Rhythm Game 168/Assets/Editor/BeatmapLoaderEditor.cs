using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BeatmapLoader))]
public class BeatmapLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BeatmapLoader beatmapLoader = (BeatmapLoader)target;
        DrawDefaultInspector();
        if (GUILayout.Button("LOAD BEATMAP"))
        {
            beatmapLoader.ClearEditor();
            beatmapLoader.Load();
        }
        if (GUILayout.Button("SAVE BEATMAP"))
        {
            beatmapLoader.Save();
        }
        if (GUILayout.Button("CLEAR BEATMAP"))
        {
            beatmapLoader.ClearEditor();
        }
    }

}