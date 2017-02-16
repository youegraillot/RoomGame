using UnityEngine;
using System;

[Serializable]
public class SavedAttributes
{
}

public class SavedMonoBehaviour : MonoBehaviour {
    
    SavedAttributes m_;

    void Awake()
    {
        GameManager.Subscribe(this);
    }

    public SavedAttributes GetAttributes()
    {
        return m_ as SavedAttributes;
        //return (T)Convert.ChangeType(m_, typeof(T));
    }

    public void SetAttributes(SavedAttributes INPUT)
    {
        m_ = INPUT;
    }
}
