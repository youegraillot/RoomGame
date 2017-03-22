using UnityEngine;
using System.Collections;
using System;

public class LockedDrawer : Enigma<EnigmaAttributes> {


    [SerializeField]
    GameObject m_key;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_key)
        {
            onSuccess();
            if(!savedAttributes.m_solved)
                gameObject.GetComponent<AudioSource>().Play();
        }
    }
	
	

    protected override void OnLoadAttributes()
    {
        //transform.parent.GetComponent<DrawableObject>().enabled = true;
        
        


    }
    protected override void onSuccess()
    {
        transform.parent.gameObject.AddComponent<DrawableObject>();
        
        m_key.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(m_key.GetComponent<CapsuleCollider>());
        m_key.GetComponent<MovableObject>().enabled = false;
        m_key.transform.localRotation = Quaternion.Euler(0, 90, 0);
        m_key.transform.position = transform.position;
        m_key.transform.parent = transform;
        m_key.transform.localPosition += new Vector3(0, 0, -11.1f);
    }
}
