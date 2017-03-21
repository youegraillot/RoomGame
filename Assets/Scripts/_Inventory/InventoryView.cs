using UnityEngine;

public abstract class InventoryView : MonoBehaviour
{
    [SerializeField] protected RectTransform m_content;
    [SerializeField] protected Camera m_cameraPreview;
    [SerializeField] protected RenderTexture m_previewTexture;
    [SerializeField] protected GameObject m_inventoryItemPrefab;

    [SerializeField, Range(1, 3)] float m_previewZoom = 2;

    Animator m_animator;
    protected InventoryModel m_inventoryModel;
	Transform m_previewTransform;

	bool m_isVisible;
	public virtual bool visible
	{
		get { return m_isVisible; }
		set
		{
			m_isVisible = value;

			if (value)
				m_animator.Play("Inventory_IN");
			else
				m_animator.Play("Inventory_OUT");
		}
	}

	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_previewTransform = m_cameraPreview.transform.GetChild(0);
	}

    protected GameObject m_selectedItem;
    public GameObject SelectedItem
    {
        get { return m_selectedItem; }
    }

	public void Init (InventoryModel model)
    {
        m_inventoryModel = model;
    }

    /// <summary>
	/// Regenerate cards.
	/// </summary>
    public abstract void updateContent();

    /// <summary>
	/// Set an item to display in the animated preview.
	/// </summary>
    public void selectPreview(GameObject obj, bool active)
    {
        if (active && m_selectedItem != obj)
        {
            selectPreview(m_selectedItem, false);
            placeForPreview(obj.transform);
            obj.GetComponent<Rigidbody>().isKinematic = true;

            m_selectedItem = obj;
        }
        else if ( !active && obj != null)
        {
            obj.SetActive(false);
            obj.GetComponent<Rigidbody>().isKinematic = false;
            m_selectedItem = null;
        }
    }

    /// <summary>
	/// Place the item in front of camera.
	/// </summary>
    protected void placeForPreview(Transform item)
    {
        item.gameObject.SetActive(true);
		m_previewTransform.localPosition = Vector3.forward * m_previewZoom * item.GetComponentInChildren<Collider>().bounds.extents.magnitude;
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.Euler(-30, 140, 0);
    }
}
