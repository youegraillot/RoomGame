using UnityEngine;
using System.Collections;

public class Door_code : MonoBehaviour
{
    public GameObject[] m_cylindres;
    [SerializeField] private int[] m_code;
    private Cylinder_code[] m_tabSrc;

	void Start ()
    {
        m_code =new int [4] { 7,3,4,8};
        m_tabSrc = new Cylinder_code[4] 
        {   m_cylindres[0].GetComponent<Cylinder_code>(),
            m_cylindres[1].GetComponent<Cylinder_code>(),
            m_cylindres[2].GetComponent<Cylinder_code>(),
            m_cylindres[3].GetComponent<Cylinder_code>()};

    }

    void OnMouseDown()
    {
        if (m_tabSrc[0].getValue() == m_code[0]
            && m_tabSrc[1].getValue() == m_code[1]
            && m_tabSrc[2].getValue() == m_code[2]
            && m_tabSrc[3].getValue() == m_code[3])
        {
            Debug.Log("good");
        }
    }
}
