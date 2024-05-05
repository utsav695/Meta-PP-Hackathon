using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVFX : MonoBehaviour
{
    private float emissionValue;

    public void setEmissionValue(float value)
    {
        emissionValue = value;
        //ParticleSystem ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = transform.GetChild(0).GetComponent<ParticleSystem.EmissionModule>(); 
        em.rateOverTime= emissionValue;
    }
}
