using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    InventoryView m_inventoryView;
    InventoryModel m_inventoryModel;
    
    public bool visible
    {
        get { return m_inventoryView.gameObject.activeSelf; }
        set
        {
            m_inventoryView.gameObject.SetActive(value);

            if (value)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = value;
        }
    }

    void Start()
    {
        m_inventoryModel = new InventoryModel();
        m_inventoryView.Init(m_inventoryModel);
    }

    public void pick()
    {
        m_inventoryView.UpdateContent();
    }

    public void add(GameObject obj)
    {
        m_inventoryModel.add(obj);
        obj.transform.SetParent(transform);
        m_inventoryView.UpdateContent();
    }

    void remove()
    {
        m_inventoryView.UpdateContent();
    }
}
