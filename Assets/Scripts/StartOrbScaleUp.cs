using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOrbScaleUp : MonoBehaviour
{
    private float startSize = 3;
    public float min = 3;
    public float max = 10;

    public void SetEmissionValue(float t)
    {
        startSize = Mathf.Lerp(min, max, t);
        ParticleSystem ps = transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule pm = ps.main;
        pm.startSize = startSize;
    }
}
