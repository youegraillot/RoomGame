using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : MonoBehaviour
{

    protected bool m_isActivated;
    public abstract bool Activate();
}
