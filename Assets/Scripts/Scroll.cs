using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour
{
    public float timeToLit = 3.0f;
    private Renderer rend;
    private bool isLit = false;
    private float litTime = 0.0f;
    private Light LightProperty;
    // Use this for initialization
    void Start()
    {
        rend = transform.GetChild(0).gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLit && litTime <= timeToLit)
        {
            litTime += Time.deltaTime;
            Debug.Log(litTime);
            rend.material.SetFloat("_ProximityLight", litTime / timeToLit);
        }

    }

    void setLitState(bool state) { isLit = state; }
    void OnTriggerEnter(Collider other)
    {
        if (LightProperty = other.gameObject.GetComponent<Light>())
        {
            isLit = true;

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Light>())
        {
            isLit = false;
        }
    }
}