using UnityEngine;
using System;

[Serializable]
public class E_RatAttributes : EnigmaAttributes
{
}

public class E_Rat : Enigma<E_RatAttributes>
{
    [SerializeField]
    Transform    m_target;
    NavMeshAgent m_navMeshAgent;
    float        m_sightDistance = 0.5f; // distance (in Unity units) at which the cheese will be out of sight

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        if ( m_target == null)
            throw new Exception("No target");
    }
	
	// Update is called once per frame
	void Update()
    {
        float dst = Vector3.Distance( transform.position, m_target.position );
        
        if ( dst > 0.0f && dst <= m_sightDistance ) {
            m_navMeshAgent.destination = m_target.position;
        }
    }

    void OnTriggerEnter( Collider other )
    {
        if( enabled && other.gameObject.name == "BoutonPressure" )
        {
            answer(true);

            enabled = false;
        }
    }
}
