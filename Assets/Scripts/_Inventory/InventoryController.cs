using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    InventoryView m_inventoryView;
    InventoryModel m_inventoryModel;

    [SerializeField]
    Transform m_Objects;
    
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
        m_inventoryModel = new InventoryModel();
        m_inventoryView.Init(m_inventoryModel);
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
    }

    void remove(GameObject obj)
    {
        m_inventoryModel.remove(obj);
        m_inventoryView.updateContent();
    }
}
