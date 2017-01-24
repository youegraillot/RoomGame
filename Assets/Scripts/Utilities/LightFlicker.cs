using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    Vector3 m_amplitude = Vector3.one;
    [SerializeField, Range(0,1)]
    float m_minIntensity = 0.25f;
    [SerializeField, Range(0, 1)]
    float m_maxIntensity = 0.5f;
    [SerializeField, Range(0, 10)]
    float m_intensityVariationSpeed = 2f;
    [SerializeField, Range(0, 0.1f)]
    float m_movementSpeed = 0.05f;

    Light m_light;
    float m_randomSeed;
    float m_initIntensity;
    Vector3 m_initPosition;

    void Start()
    {
        m_light = GetComponent<Light>();
        m_randomSeed = Random.Range(0.0f, 65535.0f);
        m_initIntensity = m_light.intensity;
        m_initPosition = transform.localPosition;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(m_randomSeed, Time.time* m_intensityVariationSpeed);
        m_light.intensity = m_initIntensity * Mathf.Lerp(m_minIntensity, m_maxIntensity, noise);

        Vector3 randomDirection = new Vector3(m_amplitude.x * Random.Range(-1, 1), m_amplitude.y * Random.Range(-1, 1), m_amplitude.z * Random.Range(-1, 1));
        
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, m_initPosition + randomDirection, noise * m_movementSpeed * m_intensityVariationSpeed);
    }
}