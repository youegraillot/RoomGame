using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject m_optionsObj;
    [SerializeField] GameObject m_baseMenu;
    [SerializeField]
    PlayerController m_controler;

    public void resume()
    {
        if (Valve.VR.OpenVR.System != null && Valve.VR.OpenVR.System.IsTrackedDeviceConnected(0))
        {
            ((ViveController)m_controler).DisplayMenu = false;

        }
        else
        {
            ((KeyboardController)m_controler).DisplayMenu = false;
        }
        //this.gameObject.SetActive(false);
    }


    public void options()
    {
        m_baseMenu.SetActive(false);
        m_optionsObj.SetActive(true);
    }

    void OnEnable()
    {
        m_baseMenu.SetActive(true);
        m_optionsObj.SetActive(false);
        m_controler = FindObjectOfType<PlayerController>();
    }

    public void exit()
    {
        Application.Quit();
    }
    
   
}
