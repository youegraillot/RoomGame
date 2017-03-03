using UnityEngine;
using System;

[Serializable]
public class SavedAttributes { }

public abstract class SavedMonoBehaviour : MonoBehaviour
{
    protected SavedAttributes m_;

    public SavedAttributes savedAttributes
    {
        get
        {
            return m_;
        }
        set
        {
            m_ = value;
            OnLoadAttributes();
        }
    }

    protected abstract void OnLoadAttributes();
}

public abstract class SavedMonoBehaviourImpl<AttributeType> :
    SavedMonoBehaviour
    where AttributeType : SavedAttributes, new()
{
    protected new AttributeType savedAttributes
    {
        get
        {
            return base.savedAttributes as AttributeType;
        }
        set
        {
            base.savedAttributes = value;
        }
    }

    void Awake()
    {
        m_ = new AttributeType();
    }
}