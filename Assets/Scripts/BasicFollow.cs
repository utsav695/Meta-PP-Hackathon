using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    public Transform target;
    public float lerpDuration;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, (1/ lerpDuration) * Time.deltaTime);
    }
}
