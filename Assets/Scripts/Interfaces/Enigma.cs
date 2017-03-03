public abstract class Enigma<AttributeType> :
    SavedMonoBehaviourImpl<AttributeType>
    where AttributeType : SavedAttributes, new()
{
    public bool m_solved = false;

    protected virtual void answer(bool isCorrect)
    {
        m_solved = isCorrect;

        if (isCorrect)
            onSuccess();
        else
            onFail();
    }

    /// <summary>
    /// Triggered when the enigma is succeeded.
    /// </summary>
    protected abstract void onSuccess();

    /// <summary>
    /// Triggered when the enigma is failed.
    /// </summary>
    protected abstract void onFail();

    protected override void OnLoadAttributes()
    {
    }
}
