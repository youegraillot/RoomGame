using UnityEngine;
using System;

public class Lock : ActivableObject
{
    [SerializeField]
    int[] m_code = new int[4];
    [SerializeField]
    Digit[] m_digits = new Digit[4];

    Animation m_animation;

	void Start()
	{
		updateState = false;

        m_animation = GetComponent<Animation>();

    }

    /// <summary>
    /// Return true if correct.
    /// </summary>
    bool checkCode()
    {
        m_animation.Play("Door_Pivot");

        for (int digitID = 0; digitID < 4; digitID++)
		{
			if (m_digits[digitID].Value != m_code[digitID])
				return false;
		}

        return true;
    }

    /// <summary>
    /// TODO : Open door
    /// </summary>
    void unlock()
    {
        m_animation.Play("Door_Open");
        canBeActivated = false;
    }

    protected override void specificActivation()
    {
        if (checkCode())
		{
			foreach (Digit digit in m_digits)
				digit.canBeActivated = false;

			unlock();
		}
	}
}
