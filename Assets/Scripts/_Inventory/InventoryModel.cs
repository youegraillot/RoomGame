using UnityEngine;
using System.Collections.Generic;

public class InventoryModel
{
    List<GameObject> m_data;

    public List<GameObject> Data
    {
        get{ return m_data; }
    }

    public InventoryModel()
    {
        m_data = new List<GameObject>();
    }

    public void add(GameObject obj)
    {
        m_data.Add(obj);
        obj.SetActive(false);
    }

    public void remove(GameObject obj)
    {
        m_data.Remove(obj);
    }
}
