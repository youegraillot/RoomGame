using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    InventoryView m_inventoryView;
    [SerializeField]
    InventoryController m_inventoryController;

    bool m_active = false;
    public bool isActive
    {
        get { return m_active; }
        set
        {
            m_active = value;
            m_inventoryView.gameObject.SetActive(m_active);
        }
    }

    void toggle()
    {
        isActive = !isActive;
    }

    void pick()
    {

    }

    void add()
    {

    }

    void remove()
    {

    }
}
