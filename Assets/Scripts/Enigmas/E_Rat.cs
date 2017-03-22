using UnityEngine;
using System;

public class E_Rat : Enigma<EnigmaAttributes>
{
    [SerializeField]
    Transform    m_target;
    NavMeshAgent m_navMeshAgent;
    Animator m_animator;
    [SerializeField]
    float        m_sightDistance = 0.5f; // distance (in Unity units) at which the cheese will be out of sight
    bool cheeseDetected = false;
    public AudioSource sondToPlayOnTargetDetected = null;
    public AudioSource sondToPlayOnWalk = null;

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();

        if ( m_target == null)
            throw new Exception("No target");

        m_animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update()
    {
        float dst = Vector3.Distance( transform.position, m_target.position );
        
        if ( dst > 0.0f && dst <= m_sightDistance ) {
            m_navMeshAgent.destination = m_target.position;
            if (!cheeseDetected && sondToPlayOnTargetDetected != null)
            {
                cheeseDetected = true;
                sondToPlayOnTargetDetected.Play();
                sondToPlayOnWalk.Play();
            }
        }
        else
        {
            sondToPlayOnTargetDetected.Stop();
            sondToPlayOnWalk.Stop();
            cheeseDetected = false;
        }

        

        m_animator.SetFloat("Speed", m_navMeshAgent.desiredVelocity.magnitude / m_navMeshAgent.speed);
    }

    void OnTriggerEnter( Collider other )
    {
        if( enabled && other.gameObject.name == "BoutonPressure" )
        {
            sondToPlayOnWalk.Stop();
            answer(true);

            enabled = false;
        }
    }
}
