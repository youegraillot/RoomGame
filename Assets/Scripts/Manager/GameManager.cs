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

public enum ControllerType
{
	Error, Vive, Keyboard
}

public class GameManager : MonoBehaviour {

    SaveStruct m_savedDatas;
    SavedMonoBehaviour[] m_SMB;
    List<Transform> m_sceneObjectsTransform = new List<Transform>();
    string m_saveFilename = "Player.save";

	public static ControllerType controllerType = ControllerType.Error;
	[SerializeField]
	GameObject m_goVive;
	[SerializeField]
	GameObject m_goKeyboard;

	public float TotalPlayedTime
    {
        get
        {
            return m_savedDatas.Game_TotalTime;
        }
    }

    void Start()
    {
		print("Method 1 : " + UnityEngine.VR.VRDevice.isPresent);
		print("Method 2 : " + Valve.VR.OpenVR.IsHmdPresent());

		if (false)
		{
			controllerType = ControllerType.Vive;
			Destroy(m_goKeyboard);
		}
		else
		{
			controllerType = ControllerType.Keyboard;
			Destroy(m_goVive);
		}

		m_SMB = FindObjectsOfType<SavedMonoBehaviour>();

        m_sceneObjectsTransform.AddRange(GameObject.Find("Objects").GetComponentsInChildren<Transform>(true));
        m_sceneObjectsTransform.AddRange(GameObject.Find("Interractive").GetComponentsInChildren<Transform>(true));
        m_sceneObjectsTransform.AddRange(GameObject.Find("Inventory").GetComponentsInChildren<Transform>(true));
        m_sceneObjectsTransform.Sort(CompareName);
    }

    void Update () {
        m_savedDatas.Game_TotalTime += Time.deltaTime;
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

    public void save()
    {
        // Save position and rotations of all objects
        m_savedDatas.Scene_ObjectsPosition = new float[m_sceneObjectsTransform.Count][];
        m_savedDatas.Scene_ObjectsRotation = new float[m_sceneObjectsTransform.Count][];

        for (int sceneObjId = 0; sceneObjId < m_sceneObjectsTransform.Count; sceneObjId++)
        {
            m_savedDatas.Scene_ObjectsPosition[sceneObjId] = Vec3ToArray(m_sceneObjectsTransform[sceneObjId].localPosition);
            m_savedDatas.Scene_ObjectsRotation[sceneObjId] = Vec3ToArray(m_sceneObjectsTransform[sceneObjId].localEulerAngles);
        }

        // Save state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        m_savedDatas.Scene_ActivableObjectsState = new bool[Scene_ActivableObjects.Length];

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            m_savedDatas.Scene_ActivableObjectsState[sceneObjId] = Scene_ActivableObjects[sceneObjId].isActivated;

        // Save SMB attributes
        m_savedDatas.SavedAttributes = new List<SavedAttributes>();
        
        foreach (var sceneSMB in m_SMB)
            m_savedDatas.SavedAttributes.Add(sceneSMB.savedAttributes);

        // Write in file
        BlazeSave.SaveData(m_saveFilename, m_savedDatas);
    }

    public void load()
    {
        m_savedDatas = BlazeSave.LoadData<SaveStruct>(m_saveFilename);

        // Load position and rotations of all objects
        for (int sceneObjId = 0; sceneObjId < m_sceneObjectsTransform.Count; sceneObjId++)
        {
            m_sceneObjectsTransform[sceneObjId].localPosition = ArrayToVec3(m_savedDatas.Scene_ObjectsPosition[sceneObjId]);
            m_sceneObjectsTransform[sceneObjId].localEulerAngles = ArrayToVec3(m_savedDatas.Scene_ObjectsRotation[sceneObjId]);
        }

        // Load state of all ActivableObjects
        ActivableObject[] Scene_ActivableObjects = GameObject.Find("Room").GetComponentsInChildren<ActivableObject>();

        for (int sceneObjId = 0; sceneObjId < Scene_ActivableObjects.Length; sceneObjId++)
            Scene_ActivableObjects[sceneObjId].isActivated = m_savedDatas.Scene_ActivableObjectsState[sceneObjId];

        // Load SMB attributes

        for (int i = 0; i < m_SMB.Length; i++)
            m_SMB[i].savedAttributes = m_savedDatas.SavedAttributes[i];
    }
}
