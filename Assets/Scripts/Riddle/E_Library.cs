using UnityEngine;
using System.Collections;
using System;
/*HOW IT WORKS
 * 
 * 
 * 
 * 
 * 
 * 
 * */

public class E_Library : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBook;// prefab du book à changer
    [SerializeField]
    private GameObject[] m_slotsPositions;

    private uint[] m_solution;


    private Book[] m_slotsBooks;

    private Animator m_animator;
    private int m_arrayLength;

	
	void Awake ()
    {

        m_arrayLength = m_slotsPositions.Length;
        m_slotsBooks = new Book[m_arrayLength];
        m_solution = new uint[m_arrayLength];

        for (int i=0;i<m_arrayLength;i++)
        {
            //generate books here
            GameObject go=Instantiate(prefabBook, new Vector3(i, 0, 0), Quaternion.identity)as GameObject;
            go.transform.Rotate(0, 90, 0);
            go.transform.position = m_slotsPositions[i].transform.position;

            go.GetComponent<Book>().ID =(uint) i;
            go.GetComponent<Book>().library = this;

            m_solution[i] =(uint) (m_arrayLength - 1 - i);
        }

#if UNITY_EDITOR
        Debug.Log("La solution est : ");
        for(int i=0;i<m_arrayLength;i++)
        {
            Debug.Log("At pos "+m_slotsPositions[i].transform.position+" : "+m_solution[i]);
        }
#endif
    }

    /*
     * 1 ----> 0
     * sens de verif
     * */
    public void onBookPlacement(Book book)
    {
        Debug.Log("onBookPlacement func");
        for(int i=0;i<m_slotsPositions.Length;i++)
        {
            if(book.getPos()==m_slotsPositions[i].transform.position)
            {
                if(m_slotsBooks[i]!=null)
                {
                    Debug.Log("Problem at func onBookPlacement, there is already a book at emplacement " + i);
                    return;
                }
                m_slotsBooks[i] = book;
                Debug.Log("book positionned :" + i);
                //Play sound
                success();
                return;
            }
        }
    }

    public bool onPullBook(Book book)
    {
        Debug.Log("pull book");
        for(int i=0;i<m_slotsBooks.Length;i++)
        {
            if(book==m_slotsBooks[i])
            {
                Debug.Log("book out : "+book.ID);
                //Play sound

                m_slotsBooks[i] = null;
                return true;
            }
        }
        Debug.Log("Problem at function onPullBook; book not found in array");
        return false;
    }

    void success()
    {
        for(int i=0;i<m_arrayLength;i++)
        {
            if (!m_slotsBooks[i])
                return;
            if (m_slotsBooks[i].ID != m_solution[i])
                return;
        }
        Debug.Log("solution OK");
    }
}
