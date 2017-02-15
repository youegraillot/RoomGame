using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SavedAttributes
{
}

public class SavedMonoBehaviour<AttributeType> : MonoBehaviour {
    
    public AttributeType Attributes;

    void Awake()
    {
        GameManager.Subscribe(Attributes as SavedAttributes);
    }

    public void Load(AttributeType newAttributes)
    {
        Attributes = newAttributes;
    }
}
