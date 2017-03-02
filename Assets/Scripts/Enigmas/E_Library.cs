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
        if (GetComponentsInChildren<E_Book>().Length != m_solution.Length)
            throw new Exception("Wrong number of E_Book.");

        Attribute.answer = new int[m_solution.Length];
    }

    public void activeBook(int BookID)
    {
        // Update answer
        Attribute.answer[Attribute.indiceArray] = BookID;
        Attribute.indiceArray++;
        
        // Check solution
        if (Attribute.indiceArray == Attribute.answer.Length)
        {
            for (int i = 0; i < Attribute.answer.Length; i++)
                if (Attribute.answer[i] != m_solution[i])
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

        (GetAttributes() as E_LibraryAttributes).indiceArray = 0;
    }

    protected override void OnLoadAttributes()
    {
    }
}
