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
    InventoryView m_inventoryView;
    InventoryModel m_inventoryModel;

    [SerializeField]
    Transform m_Objects;

    List<Transform> Scene_Objects = new List<Transform>();
    
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
        m_inventoryModel  = new InventoryModel();
        m_inventoryView.Init(m_inventoryModel);

        Scene_Objects.AddRange(m_Objects.GetComponentsInChildren<Transform>());
        Attribute.takenObjectsList = new int[Scene_Objects.Count];
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
        
        Attribute.takenObjectsList[Scene_Objects.IndexOf(obj.transform)] = 1;
    }

    void remove(GameObject obj)
    {
        m_inventoryModel.remove(obj);
        m_inventoryView.updateContent();

        Attribute.takenObjectsList[Scene_Objects.IndexOf(obj.transform)] = 0;
    }

    protected override void OnLoadAttributes()
    {
        int ID = 0;

        foreach (var objID in Attribute.takenObjectsList)
        {
            if(objID == 1)
            {
                add(Scene_Objects[ID].gameObject);
            }

            ID++;
        }
    }
}
