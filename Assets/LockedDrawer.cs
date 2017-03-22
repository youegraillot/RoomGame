using UnityEngine;
using System.Collections;
using System;

public class LockedDrawer : ActivableObject {


    [SerializeField]
    GameObject m_key;

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_key)
        {
            specificActivation();
        }
    }
	
	

    protected override void specificActivation()
    {
        transform.parent.GetComponent<DrawableObject>().enabled = true;
        //transform.parent.gameObject.AddComponent<DrawableObject>();
        gameObject.GetComponent<AudioSource>().Play();
        m_key.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(m_key.GetComponent<CapsuleCollider>());
        m_key.GetComponent<MovableObject>().enabled = false;
        m_key.transform.rotation = transform.rotation;
        m_key.transform.position = transform.position;
        m_key.transform.parent = transform;


    }
}
