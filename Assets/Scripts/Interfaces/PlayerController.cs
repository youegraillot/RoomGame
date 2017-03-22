using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    bool m_isHolding;
    bool m_isRotating;
	bool m_isDrawing;
    InteractiveObject m_currentObject;

    [SerializeField]
    InventoryController m_inventoryController;

    [SerializeField]
    protected Joint m_holdPoint;

    /// <summary>
    /// Targeted object
    /// </summary>
    protected InteractiveObject Target
    {
        get
        {
            return m_currentObject;
        }
        set
        {
            if (!HoldState && !DrawState && !RotateState)
                m_currentObject = value;
        }
    }

	/// <summary>
	/// Hold if true
	/// </summary>
	protected bool HoldState
	{
		get
		{
			return m_isHolding;
		}
		set
		{
			if (m_isHolding != value)
			{
				m_isHolding = value;

				if (value)
                {
                    Target.transform.position = m_holdPoint.transform.position;

                    m_holdPoint.connectedBody = Target.GetComponent<Rigidbody>();
                    (m_currentObject.GetComponent<MovableObject>()).isFreezed = false;
                }
                else
                {
                    m_holdPoint.connectedBody = null;
                }
            }
		}
	}

	/// <summary>
	/// Rotate if true
	/// </summary>
	protected bool RotateState
    {
		get
        {
            return m_isRotating;
        }
        set
        {
            if(m_isRotating != value)
            {
                m_isRotating = value;

                if (value)
                {
                    m_currentObject.GetComponent<MovableObject>().initRotation();
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

	/// <summary>
	/// Draw if true
	/// </summary>
	protected bool DrawState
	{
		get
		{
			return m_isDrawing;
		}
		set
		{
			if (m_isDrawing != value)
			{
				m_isDrawing = value;

				if (value)
				{
                    m_currentObject.GetComponent<DrawableObject>().initDraw(getControllerPosition());
					Cursor.lockState = CursorLockMode.None;
				}
				else
					Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

    /// <summary>
	/// 
	/// </summary>
	protected bool DisplayInventory
    {
        get
        {
            return m_inventoryController.visible;
        }
        set
        {
            if (m_inventoryController.visible != value)
                m_inventoryController.visible = value;
        }
    }

    public virtual void Update()
    {
        updateTarget();

        if (m_currentObject && !m_inventoryController.visible)
        {
            if (RotateState)
                rotateObject();

			if (DrawState)
				drawObject();
		}

		eventHandler();
	}

    /// <summary>
    /// Return controller position
    /// </summary>
    protected abstract Vector3 getControllerPosition();

    /// <summary>
    /// Called by update in order to set the target object.
    /// </summary>
    public abstract void updateTarget();

    /// <summary>
    /// Called by update in order to rotate the target object.
    /// </summary>
    public abstract void rotateObject();

	/// <summary>
	/// Called by update in order to draw the target object.
	/// </summary>
	public abstract void drawObject();

    /// <summary>
	/// Call pickFromInventory()
	/// </summary>
    public abstract void pickFromInventoryCallBack();

    /// <summary>
    /// Handle various events.
    /// </summary>
    protected abstract void eventHandler();

    /// <summary>
    /// Define if the object needs to be freezed. Acts like a switch.
    /// </summary>
    protected void freezeObject()
    {
        if (m_currentObject.GetComponent<MovableObject>() != null)
		{
			m_currentObject.GetComponent<MovableObject>().isFreezed = !m_currentObject.GetComponent<MovableObject>().isFreezed;
		}
    }

	/// <summary>
	/// Activate the ActivableObject.
	/// </summary>
	protected void activate()
    {
		if (m_currentObject != null)
            m_currentObject.GetComponent<ActivableObject>().activate();
	}

    /// <summary>
	/// Add to inventory the MovableObject.
	/// </summary>
	protected void addToInventory()
    {
        if (m_currentObject != null)
            m_inventoryController.add(m_currentObject.gameObject);
    }

    /// <summary>
	/// Pick from inventory and hold the item.
	/// </summary>
	protected bool pickFromInventory()
    {
        if(m_inventoryController.visible)
        {
            GameObject picked = m_inventoryController.pick();

            if (picked != null)
            {
                DisplayInventory = false;
                m_currentObject = picked.GetComponent<InteractiveObject>();
                HoldState = true;

                return true;
            }
        }

        return false;
    }
}