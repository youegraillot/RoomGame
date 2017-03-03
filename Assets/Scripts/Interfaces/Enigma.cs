using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnigmaAttributes : SavedAttributes
{
    public bool m_solved = false;
}

public abstract class Enigma<AttributeType> :
    SavedMonoBehaviourImpl<AttributeType>
    where AttributeType : EnigmaAttributes, new()
{
    [SerializeField]
    Renderer m_solutionRenderer;

    float m_fadeTime = 3.0f;
    float m_currentTime = 0.0f;

    protected virtual void answer(bool isCorrect)
    {
        savedAttributes.m_solved = isCorrect;

        if (isCorrect)
            onSuccess();
        else
            onFail();
    }

    protected IEnumerator Reveal()
    {
        while (m_currentTime <= m_fadeTime)
        {
            m_currentTime += Time.deltaTime;
            m_solutionRenderer.material.SetFloat("_Ratio", m_currentTime / m_fadeTime);
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// Triggered when the enigma is succeeded.
    /// </summary>
    protected virtual void onSuccess()
    {
        StartCoroutine(Reveal());
    }

    /// <summary>
    /// Triggered when the enigma is failed.
    /// </summary>
    protected virtual void onFail()
    {
    }

    protected override void OnLoadAttributes()
    {
        if (savedAttributes.m_solved)
            onSuccess();
    }
}
