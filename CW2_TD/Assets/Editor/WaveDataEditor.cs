using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveData))]
public class WaveDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //"Creates" the inspector we will be using.
        EditorGUILayout.Space(); // Its just a space, just looks nice for readability.

        if(GUILayout.Button("Clear All Spawns"))
        {
            WaveData Wave = (WaveData)target;

            Wave.Spawns.Clear(); //The actual function of our editor, resets the waves.

            EditorUtility.SetDirty(Wave); //"Dirty" is a sort of condition placed on something, it tells unity that its been changed and it needs to be saved in its new, current state.
        }
    }

}
