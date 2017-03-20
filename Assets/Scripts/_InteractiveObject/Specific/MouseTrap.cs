using UnityEngine;

public class MouseTrap : ActivableObject
{
    [SerializeField]
    GameObject m_cheese;
    [SerializeField]
    Transform m_pivot;
    [SerializeField]
    InventoryController m_inventoryController;
    [SerializeField, Range(0,0.1f)]
    float m_force;

    Rigidbody m_rigidbody;
    
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    protected override void specificActivation()
    {
        // Impulse
        m_rigidbody.AddForceAtPosition(Vector3.up * m_force, m_cheese.transform.position, ForceMode.Impulse);
        m_pivot.localEulerAngles = new Vector3(180, m_pivot.localEulerAngles.y, m_pivot.localEulerAngles.z);

        unlockCheese();
        m_inventoryController.add(m_cheese);

        FindObjectOfType<E_Rat>().notifyUnlockedCheese();
    }

    public void unlockCheese()
    {
        m_cheese.GetComponent<Rigidbody>().isKinematic = false;
        m_cheese.GetComponent<Collider>().enabled = true;
    }
}
