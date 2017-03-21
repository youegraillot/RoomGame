using UnityEngine;
using UnityEngine.UI;

public class InventoryViewPC : InventoryView
{

	public override void updateContent()
    {
        m_content.sizeDelta = Vector2.zero;
        selectPreview(m_selectedItem, false);

        // Clear old cards
        foreach (Transform item in m_content.transform)
            Destroy(item.gameObject);

        // Generate new ones
        foreach (GameObject item in m_inventoryModel.Data)
        {
            // Instantiate new ItemCard
            GameObject newItemCard = Instantiate(m_inventoryItemPrefab);
            newItemCard.transform.SetParent(m_content);
            newItemCard.name = item.name;

            // Extend Content viewport
            m_content.sizeDelta += m_inventoryItemPrefab.GetComponent<RectTransform>().sizeDelta;

            // Set the card Name
            newItemCard.GetComponentInChildren<Text>().text = item.name;

            // Define OnClick() callback
            GameObject tmp = item;
            newItemCard.GetComponent<Button>().onClick.AddListener(() => selectPreview(tmp, true));

            // Generate scaled Preview Image
            placeForPreview(item.transform);
            m_cameraPreview.Render();
            item.SetActive(false);

            RenderTexture.active = m_previewTexture;

            Texture2D texture = new Texture2D(m_previewTexture.width, m_previewTexture.height);
            texture.ReadPixels(new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), 0, 0);
            texture.Apply();

            newItemCard.GetComponentsInChildren<Image>()[1].sprite = Sprite.Create(texture, new Rect(0, 0, m_previewTexture.width, m_previewTexture.height), Vector2.zero);
        }
	}
}
