using UnityEngine;

public class DrawableObject : InteractiveObject
{
	[SerializeField, Range(0,1)]
	float m_velocity = 0.2f;

	Vector3 m_direction;
	Vector3 m_startPoint;
	Rigidbody m_rigidbody;

	void Start()
	{
		m_direction = transform.forward;
		m_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	public void initDraw(Vector3 newStartPoint)
	{
        print("init");
		m_startPoint = newStartPoint;
	}

	public void draw(Vector3 endPoint)
	{
        // Old method
        //m_rigidbody.AddForce(m_direction * (m_startPoint.y - endPoint.y) * m_velocity);

        m_rigidbody.velocity = Vector3.Distance(endPoint, m_startPoint) * m_direction * m_velocity;
        m_startPoint = endPoint;
    }
}
