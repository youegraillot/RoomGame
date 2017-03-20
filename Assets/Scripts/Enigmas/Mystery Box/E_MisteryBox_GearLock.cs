using UnityEngine;
using System.Collections;

public class E_MisteryBox_GearLock : MonoBehaviour {
    [SerializeField]
    GameObject m_gearKey;
    [SerializeField]
    GameObject m_door;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_gearKey)
        {
            activate();
        }
    }

    void activate()
    {
        Debug.Log("COFFRE OUVERT");
        m_door.GetComponent<ConfigurableJoint>().angularZMotion = ConfigurableJointMotion.Limited;
        Destroy(m_gearKey.GetComponent<MovableObject>());
        Destroy(m_gearKey.GetComponent<Rigidbody>());
        //m_gearKey.GetComponent<BoxCollider>().enabled = false;
       // m_gearKey.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;

        m_gearKey.transform.parent = transform;
        m_gearKey.transform.localPosition = new Vector3(-0.0149f, 0.007f, -0.0111f);
        m_gearKey.transform.localRotation = Quaternion.Euler(270, 90, 0);



    }
}
