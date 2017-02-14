using UnityEngine;

public class E_Book : ActivableObject
{
    [SerializeField]
    int m_index;
    E_Library m_library;

    Animation m_animation;

    void Start()
    {
        m_library = FindObjectOfType<E_Library>();
        m_animation = GetComponentInParent<Animation>();
    }

    /// <summary>
    /// Called at the end of "In" Animation.
    /// </summary>
    void NotifyELibrary()
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
