using UnityEngine;
using System.Collections;

public class Door_code : MonoBehaviour
{
    public Cylinder_code[] m_cylindres;
    [SerializeField] private int[] m_code;

    void OnMouseDown()
    {
        if (m_cylindres[0].getValue() == m_code[0]
            && m_cylindres[1].getValue() == m_code[1]
            && m_cylindres[2].getValue() == m_code[2]
            && m_cylindres[3].getValue() == m_code[3])
        {
            Debug.Log("oppen");
        }
        else Debug.Log("Bad code");
    }
}
