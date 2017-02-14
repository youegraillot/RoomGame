using UnityEngine;
using System;

public class E_Rat : Enigma
{
    public Transform    m_target        = null;
    public NavMeshAgent m_navMeshAgent  = null;
    public Animator     m_animator      = null;
    public float        m_sightDistance = 0.5f; // distance (in Unity units) at which the cheese will be out of sight

    public Transform    m_targetReward  = null; // TMP (cf. OnTriggerEnter)

    void Start()
    {
        if ( m_target == null || m_navMeshAgent == null )
        {
            throw new System.Exception();
        }
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
        if( this.enabled && other.gameObject.name == "BoutonPressure" )
        {
            // TODO: activer le mechanisme de la trappe

            { // TMP
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.name = "PLACEHOLDER_RAT_REWARD";
                cube.transform.position = m_targetReward.position;
                cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                cube.AddComponent<Rigidbody>();
            } // END TMP

            this.enabled = false;
        }
    }

    protected override void OnSuccess()
    {
        throw new NotImplementedException();
    }

    protected override void OnFail()
    {
        throw new NotImplementedException();
    }
}
