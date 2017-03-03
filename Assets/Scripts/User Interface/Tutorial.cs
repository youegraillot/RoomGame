using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class TutorialAttributes : SavedAttributes
{
    public int index = 0;
}

public class Tutorial : SavedMonoBehaviourImpl<TutorialAttributes>
{
    enum Action
    {
        None,
        Move,
        OpenInventory,
    }

    public bool Completed
    {
        get
        {
            return savedAttributes.index >= m_actionList.Length - 1;
        }
        set
        {
            if (value)
            {
                savedAttributes.index = m_actionList.Length;
            }
            else
            {
                savedAttributes.index = 0;
                play(savedAttributes.index);
            }
        }
    }

    Animator m_animator;

    Action m_pendingAction = Action.None;
    RawImage m_illustration;
    Text m_caption;

    [SerializeField]
    Action[] m_actionList;
    [SerializeField]
    Texture[] m_illustrationList;
    [SerializeField]
    string[] m_captionList;

	void Start () {
        m_animator = GetComponent<Animator>();

        m_illustration = GetComponentInChildren<RawImage>();
        m_caption = GetComponentInChildren<Text>();

        if (GameManager.ControllerType == typeof(KeyboardController))
        {
            play(savedAttributes.index);
        }
    }
	
	void Update ()
    {
        switch (m_pendingAction)
        {
            case Action.Move:
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    end();
                    break;
            case Action.OpenInventory:
                if (Input.GetKey(KeyCode.Tab))
                    end();
                break;
        }
    }

    void play(int index)
    {
        if(index < m_actionList.Length)
        {
            savedAttributes.index = index;

            m_animator.Play("Fade IN");

            m_pendingAction = m_actionList[savedAttributes.index];
            m_illustration.texture = m_illustrationList[savedAttributes.index];
            m_caption.text = m_captionList[savedAttributes.index];
        }
    }

    void playNext()
    {
        play(savedAttributes.index + 1);
    }

    void end()
    {
        m_animator.Play("Fade OUT");
        m_pendingAction = Action.None;
    }

    protected override void OnLoadAttributes()
    {
        gameObject.SetActive(!Completed);
    }
}
