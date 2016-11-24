using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected bool m_isHolding;
    
    private bool m_isRotating;
	private bool m_isDrawing;
	InteractiveObject m_currentObject;

    [SerializeField]
    UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController FPSController;

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
            if (!m_isHolding)
                m_currentObject = value;
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
                FPSController.enabled = !value;

                if (value)
                {
					((MovableObject)m_currentObject).initRotation();
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

	/// <summary>
	/// Rotate if true
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
				FPSController.enabled = !value;

				if (value)
				{
					((DrawableObject)m_currentObject).initDraw(Input.mousePosition);
					Cursor.lockState = CursorLockMode.None;
				}
				else
					Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

	public virtual void Update()
    {
        updateTarget();

        if (m_currentObject && m_isHolding)
        {
            moveObject();

            if (m_isRotating)
                rotateObject();
		}

		if (m_currentObject && m_isDrawing)
			drawObject();

		eventHandler();
	}

    /// <summary>
    /// Called by update in order to set the target object.
    /// </summary>
    public abstract void updateTarget();

    /// <summary>
    /// Called by update in order to move the target object.
    /// </summary>
    public abstract void moveObject();

    /// <summary>
    /// Called by update in order to rotate the target object.
    /// </summary>
    public abstract void rotateObject();

	/// <summary>
	/// Called by update in order to draw the target object.
	/// </summary>
	public abstract void drawObject();

	/// <summary>
	/// Handle various events.
	/// </summary>
	protected abstract void eventHandler();

	/// <summary>
	/// Define if the object needs to be freezed. Acts like a switch.
	/// </summary>
	protected void freezeObject()
    {
        if (m_currentObject != null)
			((MovableObject)m_currentObject).isFreezed = !(m_currentObject as MovableObject).isFreezed;
    }

	/// <summary>
	/// Activate the ActivableObject.
	/// </summary>
	protected void activate()
    {
		if (m_currentObject != null)
			((ActivableObject)m_currentObject).activate();
	}
}
