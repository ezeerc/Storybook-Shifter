using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintoS : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
