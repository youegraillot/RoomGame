using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class InventoryView : MonoBehaviour {

    [SerializeField] RectTransform m_content;
    [SerializeField] Camera m_cameraPreview;
    [SerializeField] RenderTexture m_previewTexture;
    [SerializeField] GameObject m_inventoryItemPrefab;

    [SerializeField, Range(1, 5)] float m_previewZoom = 2;

    InventoryModel m_inventoryModel;
    
	void Start ()
    {
        m_inventoryModel = new InventoryModel(this);

        //DEBUG

        m_inventoryModel.add("cube", GameObject.CreatePrimitive(PrimitiveType.Cube));
        m_inventoryModel.add("sphere", GameObject.CreatePrimitive(PrimitiveType.Sphere));
        m_inventoryModel.add("cylindre", GameObject.CreatePrimitive(PrimitiveType.Cylinder));

        UpdateContent();
    }

    public void UpdateContent()
    {
        m_content.sizeDelta = Vector2.zero;

        foreach (var item in m_inventoryModel.Data)
        {
            GameObject newItemCard = Instantiate(m_inventoryItemPrefab);
            newItemCard.transform.SetParent(m_content);

            // Place Card and extend Content viewport
            newItemCard.GetComponent<RectTransform>().anchoredPosition = m_inventoryItemPrefab.GetComponent<RectTransform>().anchoredPosition;
            newItemCard.GetComponent<RectTransform>().sizeDelta = m_inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta;
            newItemCard.GetComponent<RectTransform>().localPosition -= Vector3.up * m_content.sizeDelta.y;

            m_content.sizeDelta += newItemCard.GetComponent<RectTransform>().sizeDelta;

            // Set the card Name
            newItemCard.GetComponentInChildren<Text>().text = item.Key;

            // Generate Preview Image
            item.Value.transform.position = m_cameraPreview.transform.position + Vector3.forward * m_previewZoom;

            item.Value.SetActive(true);
            m_cameraPreview.Render();
            item.Value.SetActive(false);

            RenderTexture.active = m_previewTexture;

            Texture2D texture = new Texture2D(m_previewTexture.width, m_previewTexture.height);
            texture.ReadPixels(new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), 0, 0);
            texture.Apply();

            newItemCard.GetComponentInChildren<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), Vector2.zero);
        }
    }

    public void SelectPreview(string name)
    {
        m_inventoryModel.Data[name].transform.position = m_cameraPreview.transform.position + Vector3.forward * m_previewZoom;
    }
}
