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

    [SerializeField]
    Menu m_menu;

    void Start ()
    {
        if (!m_reticleTextureIdle)
            Debug.LogError("Please assign a texture to ReticleTextureIdle");
        if (!m_reticleTextureActive)
            Debug.LogError("Please assign a texture to ReticleTextureActive");

        m_holdPoint.transform.localPosition = transform.forward * m_holdingDistance;
    }

    public bool DisplayMenu
    {
        get
        {
            return m_menu.gameObject.activeSelf;
        }
        set
        {
            if (value)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                m_displayReticle = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                if (!DisplayInventory)
                {
                    Cursor.visible = false;
                    m_displayReticle = true;
                }
            }
            FPSController.enabled = !value;
            m_menu.gameObject.SetActive(value);
        }
    }

    public override void Update ()
    {
		// Calls updateTarget, eventHandler
		base.Update();
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
        if (m_displayReticle && Settings.Instance.m_controlerProxyData.m_reticle)
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
    KeyCode m_menuKey;
    [SerializeField]
    KeyCode m_crouch = KeyCode.LeftControl;

    [SerializeField, Range(1f,3f)]
    float m_crouchSpeed = 1;
    protected override void eventHandler()
    {
        if (!DisplayMenu)   //si menu on ne reçoit pas d'évents
        {
            // Trigger events
            if (Target)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Target is MovableObject)
                        freezeObject();
                    else if (Target is ActivableObject)
                        activate();
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
            HoldState = Input.GetMouseButton(1) && Target is MovableObject;
            DrawState = Input.GetMouseButton(1) && Target is DrawableObject;
            RotateState = Input.GetKey(m_rotateObjectKey) && Target is MovableObject;

            // Inventory events
            if (Input.GetKeyDown(m_openInventoryKey))
            {
                DisplayInventory = !DisplayInventory;
                m_displayReticle = !DisplayInventory;
            }
            if (Input.GetKeyDown(m_takeObjectKey) && Target is MovableObject)
            {
                HoldState = false;
                addToInventory();
            }
        }
        if(Input.GetKeyDown(m_menuKey))
        {
            DisplayMenu = !DisplayMenu;
        }
	}
}
