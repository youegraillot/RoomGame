using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct SaveStruct
{
    public float Game_TotalTime;

    public float[][] Scene_ObjectsPosition;
    public float[][] Scene_ObjectsRotation;

    public bool[] Scene_ActivableObjectsState;

    public List<SavedAttributes> SavedAttributes;
}

public class GameManager : MonoBehaviour {

    public SaveStruct SaveDatas;
    static SavedMonoBehaviour[] SavedMonoBehaviours;
    List<Transform> Scene_ObjectsTransform = new List<Transform>();
    string m_saveFilename = "Player.save";

    public static Type ControllerType
    {
        get
        {
            return FindObjectOfType<PlayerController>().GetType();
        }
    }
    
    public float TotalPlayedTime
    {
        get
        {
            return SaveDatas.Game_TotalTime;
        }
    }

    void Start()
    {
        SavedMonoBehaviours = FindObjectsOfType<SavedMonoBehaviour>();

        Scene_ObjectsTransform.AddRange(GameObject.Find("Objects").GetComponentsInChildren<Transform>(true));
        Scene_ObjectsTransform.AddRange(GameObject.Find("Interractive").GetComponentsInChildren<Transform>(true));
        Scene_ObjectsTransform.AddRange(GameObject.Find("Inventory").GetComponentsInChildren<Transform>(true));
        Scene_ObjectsTransform.AddRange(GameObject.Find("Player").GetComponentsInChildren<Transform>(true));
        Scene_ObjectsTransform.Sort(CompareName);
    }

    void Update () {
        SaveDatas.Game_TotalTime += Time.deltaTime;
    }

    public void save()
    {
        // Save position and rotations of all objects
        SaveDatas.Scene_ObjectsPosition = new float[Scene_ObjectsTransform.Count][];
        SaveDatas.Scene_ObjectsRotation = new float[Scene_ObjectsTransform.Count][];

        for (int sceneObjId = 0; sceneObjId < Scene_ObjectsTransform.Count; sceneObjId++)
        {
            SaveDatas.Scene_ObjectsPosition[sceneObjId] = Vec3ToArray(Scene_ObjectsTransform[sceneObjId].localPosition);
            SaveDatas.Scene_ObjectsRotation[sceneObjId] = Vec3ToArray(Scene_ObjectsTransform[sceneObjId].localEulerAngles);
        }

        // Save state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        SaveDatas.Scene_ActivableObjectsState = new bool[Scene_ActivableObjects.Length];

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            SaveDatas.Scene_ActivableObjectsState[sceneObjId] = Scene_ActivableObjects[sceneObjId].isActivated;

        // Save SMB attributes
        SaveDatas.SavedAttributes = new List<SavedAttributes>();
        
        foreach (var sceneSMB in SavedMonoBehaviours)
            SaveDatas.SavedAttributes.Add(sceneSMB.GetAttributes());

        // Write in file
        BlazeSave.SaveData(m_saveFilename, SaveDatas);
    }

    public void load()
    {
        SaveDatas = BlazeSave.LoadData<SaveStruct>(m_saveFilename);

        // Load position and rotations of all objects
        for (int sceneObjId = 0; sceneObjId < Scene_ObjectsTransform.Count; sceneObjId++)
        {
            Scene_ObjectsTransform[sceneObjId].localPosition = ArrayToVec3(SaveDatas.Scene_ObjectsPosition[sceneObjId]);
            Scene_ObjectsTransform[sceneObjId].localEulerAngles = ArrayToVec3(SaveDatas.Scene_ObjectsRotation[sceneObjId]);
        }

        // Load state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            Scene_ActivableObjects[sceneObjId].isActivated = SaveDatas.Scene_ActivableObjectsState[sceneObjId];

        // Load SMB attributes

        for (int i = 0; i < SavedMonoBehaviours.Length; i++)
            SavedMonoBehaviours[i].SetAttributes(SaveDatas.SavedAttributes[i]);
    }

    static int CompareName(Transform A, Transform B)
    {
        return A.name.CompareTo(B.name);
    }

    float[] Vec3ToArray(Vector3 INPUT)
    {
        return new float[] { INPUT.x, INPUT.y, INPUT.z };
    }

    Vector3 ArrayToVec3(float[] INPUT)
    {
        return new Vector3(INPUT[0], INPUT[1], INPUT[2]);
    }
}
