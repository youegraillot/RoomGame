using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
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
    GameObject m_extremaLeft;
    [SerializeField]
    GameObject m_extremaRight;
    [SerializeField]
    float m_distanceBetweenBooks;
    [SerializeField]
    float m_heightPosition;


    [SerializeField]
    GameObject prefabBook;// prefab du book à changer


    private List<Book> m_bookList;
    void Start()
    {
        m_bookList = new List<Book>();
        float x_dist = m_extremaLeft.transform.localPosition.x - m_extremaRight.transform.localPosition.x;
        float distanceX = x_dist;
        int counter = 0;
        while (distanceX >= m_distanceBetweenBooks)
        {
            Vector3 pos = new Vector3(m_extremaRight.transform.position.x + m_distanceBetweenBooks * counter,
            m_heightPosition, m_extremaRight.transform.position.z);
            GameObject go = Instantiate(prefabBook,pos,Quaternion.identity) as GameObject;
            go.transform.Rotate(0, 180, 0);
            m_bookList.Add(go.GetComponent<Book>());
            distanceX -= m_distanceBetweenBooks;
            counter++;
        }

        for(int i=0;i<m_bookList.Count-1;i++)
        {
            m_bookList[i].leftBook = m_bookList[i + 1];
        }
    }

    void Update()
    {
        foreach(Book book in m_bookList)
        {
            if (!book.isInLibrary)
                return;
            if(book.leftBook!=null)
            {
                if (book.transform.position.x > book.leftBook.transform.position.x)
                    return;
            }
        }
        Debug.Log("soluce ok");
    }

}