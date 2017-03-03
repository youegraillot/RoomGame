using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager m_target = target as GameManager;

        if (GUILayout.Button("Save"))
            m_target.save();

        if (GUILayout.Button("Load"))
            m_target.load();
    }
}
