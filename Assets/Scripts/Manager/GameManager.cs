using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static System.Type ControllerType
    {
        get
        {
            return FindObjectOfType<PlayerController>().GetType();
        }
    }

    static float m_totalGameTime;
    public static float TotalPlayedTime
    {
        get
        {
            return m_totalGameTime;
        }
    }
	
	void Update () {
        m_totalGameTime += Time.deltaTime;
    }

    void load()
    {

    }

    void save()
    {

    }
}
