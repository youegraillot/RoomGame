using UnityEngine;
using System.Collections.Generic;

public class InventoryModel
{
    Dictionary<string,GameObject> m_data;

    public Dictionary<string, GameObject> Data
    {
        get{ return m_data; }
    }

    public InventoryModel()
    {
        m_data = new Dictionary<string, GameObject>();
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
