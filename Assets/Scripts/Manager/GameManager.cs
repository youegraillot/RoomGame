using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct SaveStruct
{
    public float Game_TotalTime;
    public int Tutorial_Index;

    public float[][] Scene_ObjectsPosition;
    public float[][] Scene_ObjectsRotation;

    public bool[] Scene_ActivableObjectsState;

    public List<SavedAttributes> Enigma_SavedAttributes;
}

public class GameManager : MonoBehaviour {

    public SaveStruct SaveDatas;
    static List<SavedAttributes> Enigma_SceneAttributes;
    string m_saveFilename = "Player.bin";

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

    public static void Subscribe(SavedAttributes INPUT)
    {
        if(Enigma_SceneAttributes == null)
            Enigma_SceneAttributes = new List<SavedAttributes>();

        Enigma_SceneAttributes.Add(INPUT as SavedAttributes);
        print(INPUT);
    }

    void Update () {
        SaveDatas.Game_TotalTime += Time.deltaTime;
    }

    public void load()
    {
        SaveDatas = BlazeSave.LoadData<SaveStruct>(m_saveFilename);

        // Load position and rotations of all objects
        Transform[] Scene_ObjectsTransform = GameObject.Find("Room").GetComponentsInChildren<Transform>();

        for (int sceneObjId = 0; sceneObjId < Scene_ObjectsTransform.Length; sceneObjId++)
        {
            Scene_ObjectsTransform[sceneObjId].position = ArrayToVec3(SaveDatas.Scene_ObjectsPosition[sceneObjId]);
            Scene_ObjectsTransform[sceneObjId].eulerAngles = ArrayToVec3(SaveDatas.Scene_ObjectsRotation[sceneObjId]);
        }

        // Load state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            Scene_ActivableObjects[sceneObjId].isActivated = SaveDatas.Scene_ActivableObjectsState[sceneObjId];

        // Load enigmas attributes
        //SavedAttributes.instances = SaveDatas.Enigma_SavedAttributes;
        //SavedAttributes.Load(SaveDatas.Enigma_SavedAttributes);

        Enigma_SceneAttributes = SaveDatas.Enigma_SavedAttributes;

        string Scene = "", Saved = "", Memory = "";

        foreach (var item in FindObjectOfType<E_Library>().Attributes.answer)
            Scene += item;

        foreach (var item in (Enigma_SceneAttributes[0] as E_LibraryAttributes).answer)
            Saved += item;

        foreach (var item in (SaveDatas.Enigma_SavedAttributes[0] as E_LibraryAttributes).answer)
            Memory += item;

        print("scene : " + Scene);
        print("saved : " + Saved);
        print("memory : " + Memory);
    }

    public void save()
    {
        // Save position and rotations of all objects
        Transform[] Scene_ObjectsTransform = GameObject.Find("Room").GetComponentsInChildren<Transform>();

        SaveDatas.Scene_ObjectsPosition = new float[Scene_ObjectsTransform.Length][];
        SaveDatas.Scene_ObjectsRotation = new float[Scene_ObjectsTransform.Length][];

        for (int sceneObjId = 0; sceneObjId < Scene_ObjectsTransform.Length; sceneObjId++)
        {
            SaveDatas.Scene_ObjectsPosition[sceneObjId] = Vec3ToArray(Scene_ObjectsTransform[sceneObjId].position);
            SaveDatas.Scene_ObjectsRotation[sceneObjId] = Vec3ToArray(Scene_ObjectsTransform[sceneObjId].eulerAngles);
        }

        // Save state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        SaveDatas.Scene_ActivableObjectsState = new bool[Scene_ActivableObjects.Length];

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            SaveDatas.Scene_ActivableObjectsState[sceneObjId] = Scene_ActivableObjects[sceneObjId].isActivated;

        // Save enigmas attributes
        SaveDatas.Enigma_SavedAttributes = Enigma_SceneAttributes;

        string Scene = "", Saved = "", memory = "";

        foreach (var item in FindObjectOfType<E_Library>().Attributes.answer)
            Scene += item;

        foreach (var item in (SaveDatas.Enigma_SavedAttributes[0] as E_LibraryAttributes).answer)
            Saved += item;

        foreach (var item in (Enigma_SceneAttributes[0] as E_LibraryAttributes).answer)
            memory += item;

        print("scene : " + Scene);
        print("saved : " + Saved);
        print("memory : " + memory);

        // Write in file
        BlazeSave.SaveData(m_saveFilename, SaveDatas);
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
