using UnityEngine;
using System;

[Serializable]
public class E_LibraryAttributes : SavedAttributes
{
    public int[] answer;
    public int indice;
}

public class E_Library : Enigma<E_LibraryAttributes>
{
    [SerializeField]
    public int[] m_solution;

    void Start()
    {
        if (GetComponentsInChildren<E_Book>().Length != m_solution.Length)
            throw new Exception("Wrong number of E_Book.");

        savedAttributes.answer = new int[m_solution.Length];
    }

    public void activeBook(int BookID)
    {
        // Update answer
        savedAttributes.answer[savedAttributes.indice] = BookID;
        savedAttributes.indice++;
        
        // Check solution
        if (savedAttributes.indice == savedAttributes.answer.Length)
        {
            for (int i = 0; i < savedAttributes.answer.Length; i++)
                if (savedAttributes.answer[i] != m_solution[i])
                {
                    answer(false);
                    return;
                }

            answer(true);
        }
    }

    protected override void onSuccess()
    {
        // TODO : Unlock Number
    }

    protected override void onFail()
    {
        foreach (var book in GetComponentsInChildren<E_Book>())
            book.deactivate();

        savedAttributes.indice = 0;
    }
}
