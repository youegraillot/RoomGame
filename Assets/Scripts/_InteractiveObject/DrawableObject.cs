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
		m_rigidbody.AddForce(m_direction * (m_startPoint.y - endPoint.y) * m_velocity);
		m_startPoint = endPoint;

        
	}
}
