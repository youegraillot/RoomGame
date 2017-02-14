using UnityEngine;
using System;

public abstract class Enigma : MonoBehaviour
{
    public bool Solved = false;
    
    protected virtual void Answer(bool Correct)
    {
        Solved = Correct;

        if (Correct)
            OnSuccess();
        else
            OnFail();
    }

    /// <summary>
    /// Triggered when the enigma is succeeded.
    /// </summary>
    protected abstract void OnSuccess();

    /// <summary>
    /// Triggered when the enigma is failed.
    /// </summary>
    protected abstract void OnFail();
}
