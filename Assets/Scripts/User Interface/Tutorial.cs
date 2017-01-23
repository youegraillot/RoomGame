using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    enum Action
    {
        None,
        Move,
        OpenInventory,
    }

    int m_index = 0;

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
            play(m_index);
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
            m_index = index;

            m_animator.Play("Fade IN");

            m_pendingAction = m_actionList[m_index];
            m_illustration.texture = m_illustrationList[m_index];
            m_caption.text = m_captionList[m_index];
        }
    }

    void playNext()
    {
        play(m_index+1);
    }

    void end()
    {
        m_animator.Play("Fade OUT");
        m_pendingAction = Action.None;
    }
}
