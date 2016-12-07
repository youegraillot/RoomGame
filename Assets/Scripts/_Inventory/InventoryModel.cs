using UnityEngine;
using System.Collections.Generic;

public class InventoryModel
{
    InventoryView m_inventoryView;

    Dictionary<string,GameObject> m_data;

    public Dictionary<string, GameObject> Data
    {
        get{ return m_data; }
    }

    public InventoryModel(InventoryView inventoryView)
    {
        m_inventoryView = inventoryView;

        m_data = new Dictionary<string, GameObject>();
    }

    void notifyView()
    {
        m_inventoryView.UpdateContent();
    }

    public void add(string name, GameObject obj)
    {
        m_data.Add(name, obj);
    }

    public void remove(string name)
    {
        m_data.Remove(name);
    }
}
