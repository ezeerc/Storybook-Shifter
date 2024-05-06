using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintoS : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }



}
