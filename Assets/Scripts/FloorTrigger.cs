using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public GameObject enviornment; 
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("RightHand") || other.CompareTag("LeftHand") ) && other.transform.position != Vector3.zero)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            enviornment.gameObject.SetActive(true);
            MainManager.Instance.GoToNextSequence();
        }   
    }
}
