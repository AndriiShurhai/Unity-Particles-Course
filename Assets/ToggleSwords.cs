using UnityEngine;

public class ToggleSwords : MonoBehaviour
{
    [SerializeField] private KeyCode fireSwordToggleKey;
    [SerializeField] private KeyCode waterSwordToggleKey;

    [SerializeField] private KeyCode fireEffectToggleKey;
    [SerializeField] private KeyCode waterEffectToggleKey;


    [SerializeField] private GameObject fireSword;
    [SerializeField] private GameObject waterSword;
    [SerializeField] private ParticleSystem[] fireEffects;
    [SerializeField] private ParticleSystem[] waterEffects;


    private bool isFireSwordEnable = true;
    private bool isWaterSwordEnable = true;
    private bool isFireEffectEnable = true;
    private bool isWaterEffectEnabled = true;


    private void Update()
    {
        if (Input.GetKeyDown(fireSwordToggleKey))
        {
            isFireSwordEnable = !isFireSwordEnable;
            fireSword.SetActive(isFireSwordEnable);
        }

        else if (Input.GetKeyDown(waterSwordToggleKey))
        {
            isWaterSwordEnable = !isWaterSwordEnable;
            waterSword.SetActive(isWaterSwordEnable);
        }

        else if (Input.GetKeyDown(fireEffectToggleKey))
        {
            isFireEffectEnable = !isFireEffectEnable;
            foreach (ParticleSystem particle in fireEffects)
            {
                if (isFireEffectEnable) particle.Play();
                else particle.Stop();
            }
        }

        else if (Input.GetKeyDown(waterEffectToggleKey))
        {
            isWaterEffectEnabled = !isWaterEffectEnabled;
            foreach (ParticleSystem particle in waterEffects)
            {
                if (isWaterEffectEnabled) particle.Play();
                else particle.Stop();
            }
        }
    }
}
