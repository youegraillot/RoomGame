using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject m_optionsObj;
    [SerializeField] GameObject m_baseMenu;
    [SerializeField] KeyboardController m_controler;
    public void resume()
    {
        //m_controler.DisplayMenu = false;
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
    }

    public void exit()
    {
        Application.Quit();
    }
    
   
}
