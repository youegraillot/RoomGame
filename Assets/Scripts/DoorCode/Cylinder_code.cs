using UnityEngine;
using System.Collections;

public class Cylinder_code : MonoBehaviour {

    private int m_value;

	void Start ()
    {
        m_value = 0;
	}

    void OnMouseDown()
    {
        if (m_value < 9)
            m_value++;
        else
            m_value = 0;

        transform.Rotate(new Vector3(1,0,0),-36.0f,Space.Self);
    }

    public int getValue()
    {
        return m_value;
    }
}
