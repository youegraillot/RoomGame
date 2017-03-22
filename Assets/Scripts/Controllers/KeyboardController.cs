using UnityEngine;

public class KeyboardController : PlayerController
{
    [SerializeField]
    GameObject m_menu;

    [SerializeField]
    UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController FPSController;

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
    [SerializeField]
    Texture m_reticleTextureInteractive;
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
			InteractiveObject newTarget = m_targeInfo.collider.gameObject.GetComponent<InteractiveObject>();
            
            
            if (newTarget)
            {
                if (newTarget != Target)
                    Target = newTarget;
            }
            // Target is not MoveableObject
            
            else
            {
                if (m_targeInfo.collider.transform.parent.GetComponent<InteractiveObject>())
                    Target = m_targeInfo.collider.transform.parent.GetComponent<InteractiveObject>();
                else
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
    /// Override to define how the object should rotate.
    /// </summary>
    public override void rotateObject()
    {
        Vector3 newAngle = new Vector2(Mathf.Lerp(-135, 135, Input.mousePosition.x / Screen.width),
                                       Mathf.Lerp(-135, 135, Input.mousePosition.y / Screen.height));

		Target.GetComponent<MovableObject>().rotate(Quaternion.Euler(newAngle.y, -newAngle.x, 0));
    }

	/// <summary>
	/// Override to define how the object should be drawn.
	/// </summary>
	public override void drawObject()
	{
        Target.GetComponent<DrawableObject>().draw(Input.mousePosition);
	}

	/// <summary>
	/// Draw the reticle.
	/// </summary>
	void OnGUI()
    {
        if (m_displayReticle && Settings.Instance.m_controlerProxyData.m_reticle)
        {
            Rect screenCenter = new Rect(Screen.width / 2 - m_reticleSize / 2, Screen.height / 2 - m_reticleSize / 2, m_reticleSize, m_reticleSize);

            if (Target is ActivableObject)
                GUI.DrawTexture(screenCenter, m_reticleTextureInteractive, ScaleMode.StretchToFill, true);
            else if(Target)
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
    KeyCode m_openMenuKey;
    [SerializeField]
    KeyCode m_takeObjectKey;
    [SerializeField]
    KeyCode m_crouch = KeyCode.LeftControl;
    [SerializeField, Range(1f,3f)]
    float m_crouchSpeed = 1;
    protected override void eventHandler()
    {
        if (Input.GetKeyDown(m_openMenuKey))
        {
            DisplayMenu = !DisplayMenu;
        }
        if (DisplayMenu)
        {
            return;
        }
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
            transform.parent.localPosition = Vector3.MoveTowards(transform.parent.localPosition, transform.parent.localPosition + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f)), Time.deltaTime / 100);
        }
        else
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.up * 0.6f, Time.deltaTime * m_crouchSpeed);
        }

        GetComponentInParent<Rigidbody>().WakeUp();

        // Holding events
        if (!DisplayInventory && Target)
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
        if (Input.GetKeyDown(m_takeObjectKey) && Target != null && Target.GetComponent<MovableObject>() != null)
        {
            HoldState = false;
            addToInventory();
        }

        // Deactivate FPSController
        FPSController.enabled = !(RotateState || DrawState || DisplayInventory);
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
}

