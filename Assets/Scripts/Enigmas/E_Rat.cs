using UnityEngine;
using System;

[Serializable]
public class E_RatAttributes : EnigmaAttributes
{
    public bool isChesseUnlocked = false;
}

public class E_Rat : Enigma<E_RatAttributes>
{
    [SerializeField]
    Transform    m_target;
    NavMeshAgent m_navMeshAgent;
    Animator m_animator;
    [SerializeField]
    float        m_sightDistance = 0.5f; // distance (in Unity units) at which the cheese will be out of sight
    bool cheeseDetected = false;
    public AudioSource sondToPlayOnTargetDetected = null;

    [SerializeField]
    MouseTrap m_mouseTrap;

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
            }
        }
        else
        {
            cheeseDetected = false;
        }

        

        m_animator.SetFloat("Speed", m_navMeshAgent.desiredVelocity.magnitude / m_navMeshAgent.speed);
    }

    void OnTriggerEnter( Collider other )
    {
        if( enabled && other.gameObject.name == "BoutonPressure" )
        {
            answer(true);
            enabled = false;
        }
    }

    public void notifyUnlockedCheese()
    {
        savedAttributes.isChesseUnlocked = true;
    }

    protected override void OnLoadAttributes()
    {
        base.OnLoadAttributes();

        if (savedAttributes.isChesseUnlocked)
            m_mouseTrap.unlockCheese();
    }
}
