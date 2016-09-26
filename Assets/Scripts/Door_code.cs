using UnityEngine;
using System.Collections;

public class Door_code : MonoBehaviour
{
    public GameObject[] m_cilindres;
    private int[] m_code;

	void Start ()
    {
        m_code =new int [4] { 7,3,4,8};
	}

    public void valueChange()
    {
        if (m_cilindres[0].GetComponent<Cylinder_code>().getValue() == m_code[0]
            && m_cilindres[1].GetComponent<Cylinder_code>().getValue() == m_code[1]
            && m_cilindres[2].GetComponent<Cylinder_code>().getValue() == m_code[2]
            && m_cilindres[3].GetComponent<Cylinder_code>().getValue() == m_code[3])
        {
            Debug.Log("good");
        }
    }
}
