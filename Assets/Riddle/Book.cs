using UnityEngine;
using System.Collections;

public class Book : MoveableObject
{

    public float ValidPosX { get; set; }
    public E_Library library{get;set;}
    public bool isInLibrary { get; set; }
   
    public Book leftBook { get; set; }
    //public Book rightBook { get; set; }

    [SerializeField]
    private MeshRenderer m_childRenderer;// to change texture at runtime

    [SerializeField]
    private TextMesh m_title;// to change Title at runtime

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BookPlacement"))
        {
            isInLibrary = true;
            Debug.Log("enterInLibrary");
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BookPlacement"))
            isInLibrary = false;
    }
}
