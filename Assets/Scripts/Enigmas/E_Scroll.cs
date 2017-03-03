using UnityEngine;
using System;

[Serializable]
public class E_ScrollAttributes : SavedAttributes
{
}

public class E_Scroll : Enigma<E_ScrollAttributes>
{
    public float m_timeToLit = 3.0f;
    private Renderer m_rend;
    private bool m_isLit = false;
    private float m_litTime = 0.0f;

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
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Light>())
            m_isLit = true;
    }


    protected override void onSuccess()
    {
        throw new NotImplementedException();
    }

    protected override void onFail()
    {
        throw new NotImplementedException();
    }
}