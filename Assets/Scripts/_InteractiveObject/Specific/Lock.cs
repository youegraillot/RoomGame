using UnityEngine;
using System.Collections;

public class Lock : ActivableObject
{
    [SerializeField]
    int[] m_code = new int[4];
    [SerializeField]
    Digit[] m_digits = new Digit[4];

	void Start()
	{
		updateState = false;
	}

    /// <summary>
    /// Return true if correct.
    /// </summary>
    bool checkCode()
    {
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
        Destroy(gameObject);
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
