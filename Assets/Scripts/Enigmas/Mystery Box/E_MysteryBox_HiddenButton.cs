﻿using UnityEngine;
using System.Collections;

public class E_MysteryBox_HiddenButton : ActivableObject {


    [SerializeField]
    GameObject m_hiddenContainer;
    [SerializeField]
    GameObject m_hiddenKey;
    [SerializeField]
    Transform m_keySpawnPoint;
  
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Activate");
            activate();
        }
    }

    protected override void specificActivation()
    {
        if (!m_isActivated)
        {
            m_hiddenKey.GetComponent<Rigidbody>().isKinematic = false;
            m_hiddenContainer.SetActive(false);
            m_hiddenKey.transform.position = m_keySpawnPoint.position;
            
        }

    }

    new void specificDeactivation()
    {

    }

}