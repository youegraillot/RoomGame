using UnityEngine;
using System.Collections;

public class E_Scroll : MonoBehaviour
{
    public float m_timeToLit = 3.0f;
    private Renderer m_rend;
    private bool m_isLit = false;
    private float m_litTime = 0.0f;
    private Light m_LightProperty;
    // Use this for initialization
    void Start()
    {
        m_rend = transform.GetChild(0).gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isLit && m_litTime <= m_timeToLit)
        {
            m_litTime += Time.deltaTime;
            m_rend.material.SetFloat("_ProximityLight", m_litTime / m_timeToLit);
        }

    }

    void setLitState(bool state) { m_isLit = state; }
    void OnTriggerEnter(Collider other)
    {
        if (m_LightProperty = other.gameObject.GetComponent<Light>())
        {
            m_isLit = true;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Light>())
        {
            m_isLit = false;
        }
    }
}