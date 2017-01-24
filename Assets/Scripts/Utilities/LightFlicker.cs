using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField, Range(0,1)]
    float m_minIntensity = 0.25f;
    [SerializeField, Range(0, 1)]
    float m_maxIntensity = 0.5f;
    [SerializeField, Range(0, 10)]
    float m_speed = 2f;

    Light m_light;
    float m_randomSeed;
    float m_initIntensity;

    void Start()
    {
        m_light = GetComponent<Light>();
        m_randomSeed = Random.Range(0.0f, 65535.0f);
        m_initIntensity = m_light.intensity;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(m_randomSeed, Time.time* m_speed);
        m_light.intensity = m_initIntensity * Mathf.Lerp(m_minIntensity, m_maxIntensity, noise);
    }
}