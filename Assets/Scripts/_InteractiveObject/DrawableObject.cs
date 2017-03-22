using UnityEngine;

public class DrawableObject : InteractiveObject
{
	[SerializeField, Range(0,1)]
	float m_velocity = 0.2f;

	Vector3 m_direction;
	Vector3 m_startPoint;
	Rigidbody m_rigidbody;

    public AudioSource soundToPlay = null;

	void Start()
	{
		m_direction = transform.forward;
		m_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	public void initDraw(Vector3 newStartPoint)
	{
		m_startPoint = newStartPoint;

        if (soundToPlay != null)
        {
            soundToPlay.Play();
        }
    }

	public void draw(Vector3 endPoint)
	{
        switch (GameManager.controllerType)
        {
            case ControllerType.Vive:
                m_rigidbody.AddForce(m_direction * Vector3.Distance(m_startPoint, endPoint) * 1000 * m_velocity);
                break;
            case ControllerType.Keyboard:
                m_rigidbody.AddForce(m_direction * (m_startPoint.y - endPoint.y) * m_velocity);
                break;
        }

        m_startPoint = endPoint;
    }
}
