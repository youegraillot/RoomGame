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
        if(GameManager.controllerType == ControllerType.Vive)
        { 
            ((ViveController)m_controler).DisplayMenu = false;

        }
        else
        {
            if(GameManager.controllerType == ControllerType.Keyboard)
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
