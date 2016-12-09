using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {

    [SerializeField] RectTransform m_content;
    [SerializeField] Camera m_cameraPreview;
    [SerializeField] Transform m_originPreviewTransform;
    [SerializeField] RenderTexture m_previewTexture;
    [SerializeField] GameObject m_inventoryItemPrefab;

    [SerializeField, Range(1, 3)] float m_previewZoom = 2;

    InventoryModel m_inventoryModel;

    GameObject m_selectedItem;
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
    public void updateContent()
    {
        m_content.sizeDelta = Vector2.zero;
        selectPreview(m_selectedItem, false);

        // Clear old cards
        foreach (Transform item in m_content.transform)
            Destroy(item.gameObject);

        Debug.ClearDeveloperConsole();

        // Generate new ones
        foreach (GameObject item in m_inventoryModel.Data)
        {
            GameObject newItemCard = Instantiate(m_inventoryItemPrefab);
            newItemCard.transform.SetParent(m_content);
            newItemCard.name = item.name;

            // Place Card and extend Content viewport
            newItemCard.GetComponent<RectTransform>().anchoredPosition = m_inventoryItemPrefab.GetComponent<RectTransform>().anchoredPosition;
            newItemCard.GetComponent<RectTransform>().sizeDelta = m_inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta;
            newItemCard.GetComponent<RectTransform>().localPosition -= Vector3.up * m_content.sizeDelta.y;

            m_content.sizeDelta += newItemCard.GetComponent<RectTransform>().sizeDelta;

            // Set the card Name
            newItemCard.GetComponentInChildren<Text>().text = item.name;

            // Define OnClick() callback
            GameObject tmp = item;
            newItemCard.GetComponent<Button>().onClick.AddListener(() => selectPreview(tmp, true));

            // Generate scaled Preview Image
            placeForPreview(item.transform);

            item.SetActive(true);
            m_cameraPreview.Render();
            item.SetActive(false);

            RenderTexture.active = m_previewTexture;

            Texture2D texture = new Texture2D(m_previewTexture.width, m_previewTexture.height);
            texture.ReadPixels(new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), 0, 0);
            texture.Apply();

            newItemCard.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), Vector2.zero);
        }
    }

    /// <summary>
	/// Set an item to display in the animated preview.
	/// </summary>
    public void selectPreview(GameObject obj, bool active)
    {
        if (active && m_selectedItem != obj)
        {
            selectPreview(m_selectedItem, false);
            placeForPreview(obj.transform);
            
            obj.SetActive(true);
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
    void placeForPreview(Transform item)
    {
        m_originPreviewTransform.localPosition = m_cameraPreview.transform.position + Vector3.forward * m_previewZoom * item.GetComponentInChildren<MeshRenderer>().bounds.extents.magnitude;
        item.position = m_originPreviewTransform.position;
        item.rotation = Quaternion.Euler(-30, 140, 0);
    }
}
