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

        float temp=transform.localEulerAngles.x;
        temp += 36.0f;
        transform.Rotate(new Vector3(0,1,0),36);

        transform.parent.gameObject.GetComponent<Door_code>().valueChange();
    }

    public int getValue()
    {
        return m_value;
    }
}
