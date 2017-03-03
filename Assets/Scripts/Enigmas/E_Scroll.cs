using UnityEngine;

public class E_Scroll : Enigma<EnigmaAttributes>
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Light>())
            answer(true);
    }
}