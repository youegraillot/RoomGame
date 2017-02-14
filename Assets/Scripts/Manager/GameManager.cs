using UnityEngine;
using System;

[Serializable]
public struct SaveStruct
{
    public float Game_TotalTime;
    public int Tutorial_Index;

    public float[][] Scene_ObjectsPosition;
    public float[][] Scene_ObjectsRotation;
}

public class GameManager : MonoBehaviour {

    public static SaveStruct SaveDatas;
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

	void Update () {
        SaveDatas.Game_TotalTime += Time.deltaTime;
    }

    public void load()
    {
        SaveDatas = BlazeSave.LoadData<SaveStruct>(m_saveFilename);

        Transform[] Scene_ObjectsTransform = GameObject.Find("Room").GetComponentsInChildren<Transform>();

        for (int sceneObjId = 0; sceneObjId < Scene_ObjectsTransform.Length; sceneObjId++)
        {
            Scene_ObjectsTransform[sceneObjId].position = ArrayToVec3(SaveDatas.Scene_ObjectsPosition[sceneObjId]);
            Scene_ObjectsTransform[sceneObjId].eulerAngles = ArrayToVec3(SaveDatas.Scene_ObjectsRotation[sceneObjId]);
        }
    }

    public void save()
    {
        Transform[] SceneObjectsTransform = GameObject.Find("Room").GetComponentsInChildren<Transform>();

        SaveDatas.Scene_ObjectsPosition = new float[SceneObjectsTransform.Length][];
        SaveDatas.Scene_ObjectsRotation = new float[SceneObjectsTransform.Length][];

        for (int sceneObjId = 0; sceneObjId < SceneObjectsTransform.Length; sceneObjId++)
        {
            SaveDatas.Scene_ObjectsPosition[sceneObjId] = Vec3ToArray(SceneObjectsTransform[sceneObjId].position);
            SaveDatas.Scene_ObjectsRotation[sceneObjId] = Vec3ToArray(SceneObjectsTransform[sceneObjId].eulerAngles);
        }

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
