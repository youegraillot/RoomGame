using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
    [SerializeField, Range(1, 128)]
    int m_reticleSize = 24;

    void Start ()
    {
        if (!m_reticleTextureIdle)
            Debug.LogError("Please assign a texture to ReticleTextureIdle");
        if (!m_reticleTextureActive)
            Debug.LogError("Please assign a texture to ReticleTextureActive");

        m_holdPoint.transform.localPosition = transform.forward * m_holdingDistance;
    }
	
	public override void Update ()
    {
		// Calls updateTarget, eventHandler
		base.Update();
    }

    protected override Vector3 getControllerPosition()
    {
        return Input.mousePosition;
    }

    Ray m_ray;
    RaycastHit m_targeInfo;
    public override void updateTarget()
    {
        m_ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(m_ray, out m_targeInfo, m_raycastRange))
        {
			InteractiveObject newTarget = m_targeInfo.transform.GetComponent<InteractiveObject>();

            if (newTarget)
            {
                if (newTarget != Target)
                    Target = newTarget;
            }
            // Target is not MoveableObject
            else
                Target = null;
        }
        // No target
        else
            Target = null;

#if UNITY_EDITOR
        Debug.DrawRay(m_ray.origin, m_ray.direction * m_raycastRange, Color.blue);
#endif
    }

    /// <summary>
    /// Override to define how the object should rotate.
    /// </summary>
    public override void rotateObject()
    {
        Vector3 newAngle = new Vector2(Mathf.Lerp(-135, 135, Input.mousePosition.x / Screen.width),
                                       Mathf.Lerp(-135, 135, Input.mousePosition.y / Screen.height));

		((MovableObject)Target).rotate(Quaternion.Euler(newAngle.y, -newAngle.x, 0));
    }

	/// <summary>
	/// Override to define how the object should be drawn.
	/// </summary>
	public override void drawObject()
	{
		((DrawableObject)Target).draw(Input.mousePosition);
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

    /// <summary>
	/// Verify that we use right click to pick from inventory and display reticle.
	/// </summary>
    public override void pickFromInventoryCallBack()
    {
        if (Input.GetMouseButtonDown(1) && pickFromInventory())
            m_displayReticle = true;
    }

    /// <summary>
    /// Handle mouse and keyboard events.
    /// </summary>
    // Left Click : Freeze(MoveableObject) | Draw(Drawer)
    // Right Click : Hold(MoveableObject) | Activate(ActivableObject)
    [Header("Controls")]
    [SerializeField]
    KeyCode m_rotateObjectKey;
    [SerializeField]
    KeyCode m_openInventoryKey;
    [SerializeField]
    KeyCode m_takeObjectKey;
    [SerializeField]
    KeyCode m_crouch = KeyCode.LeftControl;
    [SerializeField, Range(1f,3f)]
    float m_crouchSpeed = 1;
    protected override void eventHandler()
    {
		// Trigger events
		if (Target)
		{
			if (Input.GetMouseButtonDown(0))
			{
                if (Target.GetComponent<ActivableObject>() != null)
                    activate();
                else if (Target.GetComponent<MovableObject>() != null)
					freezeObject();
			}
		}

        // Crouch
        if (Input.GetKey(m_crouch))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.up * -0.2f, Time.deltaTime * m_crouchSpeed);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.up * 0.6f, Time.deltaTime * m_crouchSpeed);
        }

        GetComponentInParent<Rigidbody>().WakeUp();

        // Holding events
        if(!DisplayInventory && Target)
        {
            HoldState = Input.GetMouseButton(1) && Target.GetComponent<MovableObject>() != null;
            DrawState = Input.GetMouseButton(1) && Target.GetComponent<DrawableObject>() != null;
            RotateState = Input.GetKey(m_rotateObjectKey) && Target.GetComponent<MovableObject>() != null;
        }

        // Inventory events
        if (Input.GetKeyDown(m_openInventoryKey))
        {
            DisplayInventory = !DisplayInventory;
            m_displayReticle = !DisplayInventory;
        }
        if (Input.GetKeyDown(m_takeObjectKey) && Target.GetComponent<MovableObject>() != null)
        {
            HoldState = false;
            addToInventory();
        }
	}
}
