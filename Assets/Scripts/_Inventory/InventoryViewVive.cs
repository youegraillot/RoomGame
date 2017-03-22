using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class InventoryViewVive : InventoryView
{
    void Update()
    {
        if (SteamVR_Controller.Input((int)OpenVR.System.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand)).GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            float padX = (SteamVR_Controller.Input((int)OpenVR.System.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand)).GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x  + 1) / 2;
            
            int itemCount = m_inventoryModel.getListCount() - 1;

            if (padX > 0 && padX < itemCount+1)
                selectPreview(m_inventoryModel.Data[Mathf.RoundToInt(padX * itemCount)], true);
        }
    }

    public override void updateContent()
    {
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

            newItemCard.transform.localPosition = Vector3.zero;
            newItemCard.transform.localRotation = Quaternion.identity;
            newItemCard.transform.localScale = Vector3.one;

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
