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
        SetAttributes(new E_LibraryAttributes());

        if (GetComponentsInChildren<E_Book>().Length != m_solution.Length)
            throw new Exception("Wrong number of E_Book.");

        (GetAttributes() as E_LibraryAttributes).answer = new int[m_solution.Length];
    }

    public void activeBook(int BookID)
    {
        // Update answer
        (GetAttributes() as E_LibraryAttributes).answer[(GetAttributes() as E_LibraryAttributes).indiceArray] = BookID;
        (GetAttributes() as E_LibraryAttributes).indiceArray++;
        
        // Check solution
        if ((GetAttributes() as E_LibraryAttributes).indiceArray == (GetAttributes() as E_LibraryAttributes).answer.Length)
        {
            for (int i = 0; i < (GetAttributes() as E_LibraryAttributes).answer.Length; i++)
                if ((GetAttributes() as E_LibraryAttributes).answer[i] != m_solution[i])
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
}
