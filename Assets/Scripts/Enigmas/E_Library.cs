using UnityEngine;
using System;

public class E_Library : Enigma
{
    [SerializeField]
    private int[] m_solution;
    private int[] m_answer;
    private int m_indiceArray = 0;

    void Start()
    {
        if(GetComponentsInChildren<E_Book>().Length != m_solution.Length)
            throw new Exception("Wrong number of E_Book.");

        m_answer = new int[m_solution.Length];
    }

    public void activeBook(int BookID)
    {
        // Update answer
        m_answer[m_indiceArray] = BookID;
        m_indiceArray++;
        
        // Check solution
        if (m_indiceArray == m_answer.Length)
        {
            for (int i = 0; i < m_answer.Length; i++)
                if (m_answer[i] != m_solution[i])
                {
                    Answer(false);
                    return;
                }

            Answer(true);
        }
    }

    protected override void OnSuccess()
    {
        // TODO : Unlock Number
    }

    protected override void OnFail()
    {
        foreach (var book in GetComponentsInChildren<E_Book>())
            book.deactivate();

        m_indiceArray = 0;
    }
}
