using UnityEngine;

public class Drawer : InteractiveObject
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


	//public Vector3 openingDirection = new Vector3(0, 0, 1);
	//public float openingForce = 0.25f;

	//// Use this for initialization
	//void Start()
	//{
	//	m_rigidbody = gameObject.GetComponent<Rigidbody>();
	//}

	//public override void moveTo(Vector3 newPosition)
	//{
	//	m_rigidbody.AddForce(openingDirection * (newPosition.y) * openingForce);
	//}
}
