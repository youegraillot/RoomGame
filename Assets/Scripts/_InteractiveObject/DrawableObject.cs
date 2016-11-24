using UnityEngine;

public class DrawableObject : InteractiveObject
{
	[SerializeField, Range(0,1)]
	float m_velocity = 0.2f;

	Vector3 m_direction = new Vector3(0, 0, 1);
	Vector3 m_startPoint;
	Rigidbody m_rigidbody;

	void Start()
	{
		m_rigidbody = gameObject.GetComponent<Rigidbody>();
	}

	public void initDraw(Vector3 newStartPoint)
	{
		m_startPoint = newStartPoint;
	}

	public void draw(Vector3 endPoint)
	{
		m_rigidbody.AddForce(m_direction * (endPoint.y - m_startPoint.y) * m_velocity);
		m_startPoint = endPoint;
	}
}
