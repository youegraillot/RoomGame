using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Image m_ct_img;

    [SerializeField]
    GameObject m_jouerMenu;
    [SerializeField]
    GameObject m_optionMenu;
    [SerializeField]
    GameObject m_creditsMenu;
    [SerializeField]
    GameObject m_baseMenu;


	void Awake ()
    {
        m_ct_img.type = Image.Type.Filled;
        m_ct_img.fillMethod = Image.FillMethod.Radial90;
        m_ct_img.fillOrigin = 0;
        m_ct_img.fillAmount = 0;
        //foreach(GameObject go in )
	}

    void Start()
    {
        StartCoroutine(animStr());
    }
  
    IEnumerator animStr()
    {
        float amount = 0.02f;
        while(m_ct_img.fillAmount<1)
        {
            m_ct_img.fillAmount += amount;
            yield return new WaitForSeconds(0.03f);
        }
        yield return null;
    }


    #region ButtonOnClickEventBaseMenu

    public void play()
    {
        m_baseMenu.SetActive(false);
        m_jouerMenu.SetActive(true);
    }
    public void newGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void coninueGame()
    {

    }
    public void options()
    {
        m_baseMenu.SetActive(false);
        m_optionMenu.SetActive(true);
    }
    public void credits()
    {
        m_baseMenu.SetActive(false);
        m_creditsMenu.SetActive(true);
    }
    public void backToMain()
    {
        //m_creditsMenu.SetActive(false);
        m_optionMenu.SetActive(false);
        m_jouerMenu.SetActive(false);
        m_baseMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion

}
