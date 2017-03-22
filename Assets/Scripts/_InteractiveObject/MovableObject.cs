using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : InteractiveObject
{
	Quaternion m_initialRotation;
	protected Rigidbody m_rigidbody;
    
    public bool isFreezed
    {
        get
        {
            return m_rigidbody.constraints == RigidbodyConstraints.FreezeAll;
        }
        set
        {
            if (value)
                m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            else
                m_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void initRotation()
    {
		m_initialRotation = transform.rotation;
	}
    
    public virtual void rotate(Quaternion newRotation)
    {
        transform.rotation = newRotation * m_initialRotation;
    }
}
