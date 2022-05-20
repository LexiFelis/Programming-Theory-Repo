using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the hob hazard. Will cycle between off/on using time set in inspector
public class HobFire : MonoBehaviour 
{
    [SerializeField] private float activeTime;
    [SerializeField] private float offTime;

    private ParticleSystem[] childrenParticle;
    //hitbox is a child object in 0 position
    private GameObject hitBox;

    private void Start()
    {
        hitBox = transform.GetChild(0).gameObject;
        childrenParticle = gameObject.GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(HobCycle());
    }

    private IEnumerator HobCycle()
    {
        while (true)
        {
            // hitbox and particle emmissions are enabled/disabled in sync
            hitBox.SetActive(true);
            ParticleSwitch(true);
            yield return new WaitForSeconds(activeTime);

            hitBox.SetActive(false);
            ParticleSwitch(false);
            yield return new WaitForSeconds(offTime);
        }
    }

    // For turning the flame particles on and off
    private void ParticleSwitch(bool partOnOff)
    {
        foreach(ParticleSystem childPS in childrenParticle)
        {
            ParticleSystem.EmissionModule childPSEmissionModule = childPS.emission;
            childPSEmissionModule.enabled = partOnOff;
        }
    }
}
