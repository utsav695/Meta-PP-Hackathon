using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrackTo : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position; 
        this.transform.rotation = target.rotation;
    }
}
