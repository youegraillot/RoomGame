using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableObject : InteractiveObject
{
    public AudioSource sondToPlayOnColision = null;
    float TimeBeaforPlaySound = 0.5f;

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
        TimeBeaforPlaySound += Time.time;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void initRotation()
    {
		m_initialRotation = transform.localRotation;
	}
    
    public virtual void rotate(Quaternion newRotation)
    {
        transform.localRotation = m_initialRotation * newRotation;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (sondToPlayOnColision!=null && Time.time > TimeBeaforPlaySound)
        {
            sondToPlayOnColision.Play();
        }
    }
}
