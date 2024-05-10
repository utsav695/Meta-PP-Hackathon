using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVFX : MonoBehaviour
{
    private float emissionValue;
    public float min = 0; 
    public float max = 20; 

    public void setEmissionValue(float t)
    {
        emissionValue = Mathf.Lerp(min, max, t);
        //ParticleSystem ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = transform.GetChild(0).GetComponent<ParticleSystem.EmissionModule>(); 
        em.rateOverTime= emissionValue;
    }
}
