using UnityEngine;
using System.Collections;
using System;

public class KeyboardController : PlayerController
{
    [Header("Controller Settings")]
    [SerializeField, Range(0f, 2f)]
    float m_holdingDistance = 1f;
    [SerializeField, Range(1f, 10f)]
    float m_raycastRange = 3f;

    [Header("Reticle Settings")]
    [SerializeField]
    bool m_displayReticle = true;
    [SerializeField]
    Texture m_reticleTextureIdle;
    [SerializeField]
    Texture m_reticleTextureActive;
    [SerializeField, Range(1, 32)]
    int m_reticleSize = 24;
    
    void Start ()
    {
        if (!m_reticleTextureIdle)
            Debug.LogError("Please assign a texture to ReticleTextureIdle");
        if (!m_reticleTextureActive)
            Debug.LogError("Please assign a texture to ReticleTextureActive");
    }
	
	public override void Update ()
    {
        base.Update(); // Calls moveObject and updateTarget

        if (Input.GetMouseButtonDown(0))
            freezeObject();

        m_isHolding = Input.GetMouseButton(1) && Target;

        RotateState = Input.GetKey(KeyCode.LeftControl);
    }

    Ray m_ray;
    RaycastHit m_targeInfo, m_prevTargeInfo;
    bool m_hasPreviousHit = false;

    private void updateGameObjectLayer(GameObject gameObj, string layerName)
    {
        gameObj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in gameObj.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(layerName);
        }
    }

    public override void updateTarget()
    {
        m_ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(m_ray, out m_targeInfo, m_raycastRange))
        {
            MoveableObject newTarget = m_targeInfo.transform.GetComponent<MoveableObject>();

            if (newTarget)
            {
                if (newTarget != Target)
                {
                    if (m_hasPreviousHit)
                    {
                        updateGameObjectLayer(m_prevTargeInfo.transform.gameObject, "Default");
                        m_hasPreviousHit = false;
                    }

                    updateGameObjectLayer(m_targeInfo.transform.gameObject, "Ignore Player");

                    m_prevTargeInfo = m_targeInfo;
                    Target = newTarget;

                    m_hasPreviousHit = true;
                }
            }
            // Target is not MoveableObject
            else
            {
                if (m_hasPreviousHit)
                {
                    updateGameObjectLayer(m_prevTargeInfo.transform.gameObject, "Default");
                    m_hasPreviousHit = false;
                }
                Target = null;
            }
        }
        // No target
        else
            Target = null;

#if UNITY_EDITOR
        Debug.DrawRay(m_ray.origin, m_ray.direction * m_raycastRange, Color.blue);
#endif
    }

    /// <summary>
    /// Override to define how the object should move.
    /// </summary>
    public override void moveObject()
    {
        Target.moveTo(transform.position + transform.forward * m_holdingDistance);
    }

    /// <summary>
    /// Override to define how the object should rotate.
    /// </summary>
    public override void rotateObject()
    {
        Vector3 newAngle = new Vector2(Mathf.Lerp(-135, 135, Input.mousePosition.x / Screen.width),
                                       Mathf.Lerp(-135, 135, Input.mousePosition.y / Screen.height));

        Target.rotate(Quaternion.Euler(newAngle.y, -newAngle.x, 0));
    }

    /// <summary>
    /// Draw the reticle.
    /// </summary>
    void OnGUI()
    {
        if (m_displayReticle)
        {
            Rect screenCenter = new Rect(Screen.width / 2 - m_reticleSize / 2, Screen.height / 2 - m_reticleSize / 2, m_reticleSize, m_reticleSize);

            if (Target)
                GUI.DrawTexture(screenCenter, m_reticleTextureActive, ScaleMode.StretchToFill, true);
            else
                GUI.DrawTexture(screenCenter, m_reticleTextureIdle, ScaleMode.StretchToFill, true);
        }
    }
}
