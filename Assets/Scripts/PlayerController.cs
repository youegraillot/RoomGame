using UnityEngine;
using System.Collections;

public abstract class PlayerController : MonoBehaviour
{
    protected bool m_isHolding;
    
    private bool m_isRotating;
    MoveableObject m_currentObject;

    [SerializeField]
    UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController FPSController;

    /// <summary>
    /// Targeted object
    /// </summary>
    protected MoveableObject Target
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
                    m_currentObject.initRotation();
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
    /// Define if the object needs to be freezed. Acts like a switch.
    /// </summary>
    protected void freezeObject()
    {
        if (m_currentObject != null)
            m_currentObject.isFreezed = !m_currentObject.isFreezed;
    }

    protected void interact()
    {

    }
}
