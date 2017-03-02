

public abstract class Enigma<AttributeType> :
    SavedMonoBehaviourImpl<AttributeType>
    where AttributeType : SavedAttributes, new()
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
