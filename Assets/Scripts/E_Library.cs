using UnityEngine;
using System.Collections;

public class E_Library : MonoBehaviour
{
    [SerializeField]
    private Book[] m_bookArray;
    [SerializeField]
    private int[] m_solution;
    private int[] m_playerProposal;
    private int m_indiceArray;
    private bool canReset=true;
    void Start()
    {
        if(m_bookArray.Length!=m_solution.Length)
        {
            Debug.LogError("book_array and solution doesn't have the same size");
            return;
        }
        m_playerProposal = new int[m_bookArray.Length];
        for(int i=0;i<m_bookArray.Length;i++)
        {
            m_bookArray[i].SetLibrary(this);
        }
        m_indiceArray = 0;
    }

    public void resetProposal()
    {
        if (canReset)
        {
            for (int i = 0; i < m_playerProposal.Length; i++)
            {
                m_playerProposal[i] = -1;
                m_bookArray[i].reset();
            }
            m_indiceArray = 0;
        }
    }

    public void activeBook(Book book)
    {
        m_playerProposal[m_indiceArray] = book.GetID();
        m_indiceArray++;
        if (m_indiceArray == m_playerProposal.Length)
        {
            CheckForSolution();
        }
    }
    void CheckForSolution()
    {
        Debug.Log("check");
        for (int i=0;i<m_playerProposal.Length;i++)
        {
            if (m_playerProposal[i] != m_solution[i])
                return;
        }
        Debug.Log("soluce ok");
        canReset = false;
    }

#if UNITY_EDITOR

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            resetProposal();
            Debug.Log("reset");
        }
    }
#endif
}
