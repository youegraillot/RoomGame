using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    InventoryView m_inventoryView;
    InventoryModel m_inventoryModel;
    
    public bool visible
    {
        set
        {
            m_inventoryView.gameObject.SetActive(value);
        }
    }

    void Start()
    {
        m_inventoryModel = new InventoryModel();
        m_inventoryView.Init(m_inventoryModel);
    }

    public void pick()
    {
        
    }

    public void add(GameObject obj)
    {
        obj.transform.parent = transform;
        m_inventoryModel.add(obj.name, obj);
        m_inventoryView.UpdateContent();
    }

    public void remove()
    {

    }
}
