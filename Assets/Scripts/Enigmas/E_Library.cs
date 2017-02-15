using UnityEngine;
using System;

[Serializable]
public class E_LibraryAttributes : SavedAttributes
{
    public int[] answer;
    public int indiceArray;
}

public class E_Library : Enigma<E_LibraryAttributes>
{
    [SerializeField]
    public int[] m_solution;

    void Start()
    {
        Attributes = new E_LibraryAttributes();

        if (GetComponentsInChildren<E_Book>().Length != m_solution.Length)
            throw new Exception("Wrong number of E_Book.");

        Attributes.answer = new int[m_solution.Length];
    }

    public void activeBook(int BookID)
    {
        // Update answer
        Attributes.answer[Attributes.indiceArray] = BookID;
        Attributes.indiceArray++;
        
        // Check solution
        if (Attributes.indiceArray == Attributes.answer.Length)
        {
            for (int i = 0; i < Attributes.answer.Length; i++)
                if (Attributes.answer[i] != m_solution[i])
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

        Attributes.indiceArray = 0;
    }
}
