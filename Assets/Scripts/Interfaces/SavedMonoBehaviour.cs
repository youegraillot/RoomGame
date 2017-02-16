using UnityEngine;
using System;

[Serializable]
public class SavedAttributes { }

public abstract class SavedMonoBehaviour : MonoBehaviour {
    
    protected SavedAttributes m_;

    void Awake()
    {
        GameManager.Subscribe(this);
    }

    public SavedAttributes GetAttributes()
    {
        return m_;
    }

    public void SetAttributes(SavedAttributes INPUT)
    {
        m_ = INPUT;
    }
}
