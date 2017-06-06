using UnityEngine;
using System.Collections;

public enum E_Language
{
    None=-1,
    French,
    English
}

public class Settings
{
    public bool m_enableHalo = true;
    public float m_volume = 100;
    public bool m_enableReticle = true;
    public E_Language m_language = E_Language.French;
}
