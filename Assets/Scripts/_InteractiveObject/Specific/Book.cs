using UnityEngine;
using System.Collections;
using System;

public class Book : ActivableObject
{
    [SerializeField]
    bool m_isResetBook=false;
    private E_Library m_library;
    [SerializeField]
    private int m_id;
    [SerializeField]
    private string m_title;
    public int GetID() { return m_id; }
    public void SetLibrary(E_Library lib)
    {
        m_library = lib;
    }
    private Vector3 m_initPos;
    void Start()
    {
        if(m_title.Length>1)
        {
            GetComponentInChildren<TextMesh>().text = m_title;
        }
        m_initPos = transform.position;
    }

    protected override void specificActivation()
    {
        if (!m_isResetBook)
        {
            canBeActivated = false;
            StartCoroutine(animate(true));
            m_library.activeBook(this);
        }
        else
        {
            deactivate();
            updateState = false;
            m_library.resetProposal();
        }
    }

    protected override void specificDeactivation()
    {

    }
    /// <summary>
    /// animation function (best way is just to have an animator/animation
    /// </summary>

    IEnumerator animate(bool act)
    {
        if (act)
        {
            for(int i=0;i<4;i++)
            {
                transform.Translate(-0.01f * i, 0f, 0f);
                yield return new WaitForSeconds(0.017f+(i*0.01f));
            }
        }
        else
        {
            transform.position = m_initPos;
        }
        yield return null;
    }

    public void reset()
    {
        deactivate();
        canBeActivated = true;
        StartCoroutine(animate(false));
    }
}
