using UnityEngine;

public class E_Book : ActivableObject
{
    int m_index;
    E_Library m_library;

    Animation m_animation;

    void Start()
    {
        m_index = int.Parse(name[8].ToString());

        m_library = FindObjectOfType<E_Library>();
        m_animation = GetComponentInParent<Animation>();
    }

    /// <summary>
    /// Called at the end of "In" Animation.
    /// </summary>
    void notifyELibrary()
    {
        m_library.activeBook(m_index);
    }

    protected override void specificActivation()
    {
        m_animation.Play("In");
    }

    protected override void specificDeactivation()
    {
        m_animation.Play("Out");
    }
}
