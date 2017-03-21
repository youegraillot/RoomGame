using UnityEngine;
using System.Collections;

public class E_MisteryBox_GearLock : ActivableObject {
    [SerializeField]
    GameObject m_gearKey;
    [SerializeField]
    GameObject m_door;
    [SerializeField]
    Transform m_paperSpawn;
    [SerializeField]
    GameObject m_paper;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == m_gearKey)
        {
            specificActivation();
            m_door.GetComponent<AudioSource>().PlayDelayed(2);
            GetComponent<AudioSource>().Play();
        }
    }

    protected override void specificActivation()
    {
        Destroy(m_gearKey.GetComponent<BoxCollider>());
        m_gearKey.GetComponent<MovableObject>().enabled = false;
        m_door.GetComponent<Animation>().Play("BoxOpening");
        m_gearKey.GetComponent<Rigidbody>().isKinematic = true;
        m_paper.transform.position = m_paperSpawn.position;
        m_paper.GetComponent<Rigidbody>().isKinematic = false;
        m_gearKey.transform.parent = transform;
        m_gearKey.transform.localPosition = new Vector3(-0.0149f, 0.007f, -0.0111f);
        m_gearKey.transform.localRotation = Quaternion.Euler(270, 90, 0);
    }
}
