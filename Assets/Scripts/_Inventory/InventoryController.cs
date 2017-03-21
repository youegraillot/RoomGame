using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class InventoryAttributes : SavedAttributes
{
    public int[] takenObjectsList;
}

public class InventoryController : SavedMonoBehaviourImpl<InventoryAttributes>
{
    [SerializeField]
    InventoryViewPC m_inventoryViewPC;
    [SerializeField]
    InventoryViewVive m_inventoryViewVive;
    InventoryView m_inventoryView;
    InventoryModel m_inventoryModel = new InventoryModel();

    [SerializeField]
    Transform m_Objects;

    // Reference all the objects we can take
    List<Transform> m_sceneObjects = new List<Transform>();
    
    public bool visible
    {
        get { return m_inventoryView.visible; }
        set
        {
			m_inventoryView.visible = value;

            if (value)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = value;
        }
    }

    void Start()
    {
        if (GameManager.ControllerType == typeof(ViveController))
            m_inventoryView = m_inventoryViewVive;
        else if (GameManager.ControllerType == typeof(KeyboardController))
            m_inventoryView = m_inventoryViewPC;

        m_inventoryView.Init(m_inventoryModel);

        m_sceneObjects.AddRange(m_Objects.GetComponentsInChildren<Transform>());
        savedAttributes.takenObjectsList = new int[m_sceneObjects.Count];
    }

    /// <summary>
	/// Return the selected item.
	/// </summary>
    public GameObject pick()
    {
        GameObject picked = m_inventoryView.SelectedItem;

        if (picked != null)
        {
            remove(picked);
            picked.SetActive(true);
            picked.transform.SetParent(m_Objects);
            return picked;
        }

        return null;        
    }

    public void add(GameObject obj)
    {
        m_inventoryModel.add(obj);
        obj.transform.SetParent(transform);
        m_inventoryView.updateContent();
        
        savedAttributes.takenObjectsList[m_sceneObjects.IndexOf(obj.transform)] = 1;
    }

    void remove(GameObject obj)
    {
        m_inventoryModel.remove(obj);
        m_inventoryView.updateContent();

        savedAttributes.takenObjectsList[m_sceneObjects.IndexOf(obj.transform)] = 0;
    }

    protected override void OnLoadAttributes()
    {
        int ID = 0;

        foreach (var objID in savedAttributes.takenObjectsList)
        {
            if(objID == 1)
            {
                add(m_sceneObjects[ID].gameObject);
            }

            ID++;
        }
    }
}
