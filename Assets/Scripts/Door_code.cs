using UnityEngine;
using System.Collections;

public class Door_code : MonoBehaviour
{
    public GameObject[] m_cylindres;
    [SerializeField] private int[] m_code;

	void Start ()
    {
        m_code =new int [4] { 7,3,4,8};
	}

    void OnMouseDown()
    {
        if (m_cylindres[0].GetComponent<Cylinder_code>().getValue() == m_code[0]
            && m_cylindres[1].GetComponent<Cylinder_code>().getValue() == m_code[1]
            && m_cylindres[2].GetComponent<Cylinder_code>().getValue() == m_code[2]
            && m_cylindres[3].GetComponent<Cylinder_code>().getValue() == m_code[3])
        {
            Debug.Log("good");
        }
    }
}
