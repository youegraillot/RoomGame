using UnityEngine;
using System.Collections;

public class E_MysteryBox_Lock : MonoBehaviour {

    [SerializeField]
    GameObject m_key;
    [SerializeField]
    GameObject m_drawerActivate;
    [SerializeField]
    GameObject m_gearKey;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_key)
        {
            activate();
        }
    }

    void activate()
    {
        Debug.Log("TIROIR OUVERT");
        Destroy(m_key.GetComponent<MovableObject>());//.enabled = false;
        Destroy(m_key.GetComponent<Rigidbody>());
        m_key.transform.rotation = transform.rotation;
        m_key.transform.position = transform.position;
        m_key.transform.parent = transform;
        m_drawerActivate.transform.localPosition += new Vector3(0.035f, 0, 0);
        m_gearKey.transform.position = m_drawerActivate.transform.GetChild(0).position;
        m_gearKey.GetComponent<Rigidbody>().isKinematic = false;
    }
}
