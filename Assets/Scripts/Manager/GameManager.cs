using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_vrController;

    [SerializeField]
    GameObject m_playerController;
    void Start()
    {
        if(Valve.VR.OpenVR.IsHmdPresent()==true)
        {
            m_vrController.SetActive(true);
            Destroy(m_playerController);
        }
        else
        {
            m_playerController.SetActive(true);
            Destroy(m_vrController);
        }
    }
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
