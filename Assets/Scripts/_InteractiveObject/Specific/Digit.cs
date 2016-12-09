using UnityEngine;
using System.Collections;

public class Digit : ActivableObject
{
    Animator m_animator;
    int m_value = 0;

    Vector3 m_rotationAxis;

    public int Value
    {
        get { return m_value; }
    }

    void Start()
    {
        m_rotationAxis = transform.right;
        m_animator = GetComponent<Animator>();
    }

    protected override void specificActivation()
    {
        m_animator.Play("Rotate 36");
    }

    protected override void specificDeactivation()
    {
		transform.Rotate(m_rotationAxis, -36);
        m_value = (m_value+1) % 10;
    }
}
