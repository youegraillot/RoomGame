using UnityEngine;
using System.Collections;

public class Book : MonoBehaviour
{
    public E_Library library{get;set;}
    public uint ID { get; set; }
    Vector3 m_initialPos;
    public Vector3 getPos() { return m_initialPos; }

    private bool m_isPositionned=false;

    [SerializeField]
    private MeshRenderer m_childRenderer;// to change texture at runtime

    [SerializeField]
    private TextMesh m_title;// to change Title at runtime

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==LayerMask.NameToLayer("BookPlacement") && !m_isPositionned)//create a layer
        {
            m_isPositionned = true;
            transform.position = other.transform.position;// smooth translation to the correct position
            m_initialPos = other.transform.position;
            library.onBookPlacement(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BookPlacement"))//create a layer
        {
            library.onPullBook(this);
            m_isPositionned = false;
            m_initialPos = Vector3.zero;
        }
    }

}
