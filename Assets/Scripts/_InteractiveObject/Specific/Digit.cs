using UnityEngine;
using System.Collections;

public class Digit : ActivableObject
{
    Animator m_animator;
    int m_value = 0;
    public int Value
    {
        get { return m_value; }
    }

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    protected override void specificActivation()
    {
        m_animator.Play("Rotate 36");
    }

    protected override void specificDeactivation()
    {
		transform.Rotate(transform.right, -36);
        m_value = (m_value+1) % 10;
    }
}
