using UnityEngine;
using System.Collections;

public class Digit : ActivableObject
{
    Animation m_animation;
    int m_value = 0;

    Vector3 m_rotationAxis;

    public int Value
    {
        get { return m_value; }
    }

    void Start()
    {
        m_rotationAxis = transform.right;
        m_animation = GetComponent<Animation>();
    }

    protected override void specificActivation()
    {
        transform.Rotate(m_rotationAxis, -36);
        m_animation.Play();
    }

    protected override void specificDeactivation()
    {
        m_value = (m_value+1) % 10;
    }
}
