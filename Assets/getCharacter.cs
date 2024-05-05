using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    /*private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = transform;
            Debug.Log("Player attached to cube");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null; // Remove the parent-child relationship
            Debug.Log("Player detached from cube");
        }
    }
}
