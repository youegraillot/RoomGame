using UnityEngine;
using System;

[Serializable]
public class SavedAttributes { }

public abstract class SavedMonoBehaviour : MonoBehaviour
{
    protected SavedAttributes m_;
    
    public SavedAttributes GetAttributes()
    {
        return m_;
    }

    public void SetAttributes(SavedAttributes INPUT)
    {
        m_ = INPUT;
        OnLoadAttributes();
    }

    protected abstract void OnLoadAttributes();
}

public abstract class SavedMonoBehaviourImpl<AttributeType> :
    SavedMonoBehaviour
    where AttributeType : SavedAttributes, new()
{
    public AttributeType savedAttributes
    {
        get
        {
            return m_ as AttributeType;
        }
        set
        {
            m_ = value;
        }
    }

    void Awake()
    {
        savedAttributes = new AttributeType();
    }
}