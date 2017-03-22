using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ButtonComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    GameObject m_children;
    void Start()
    {
        m_children = transform.GetChild(0).gameObject;
        m_children.SetActive(false);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_children.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_children.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_children.SetActive(false);
    }
}
