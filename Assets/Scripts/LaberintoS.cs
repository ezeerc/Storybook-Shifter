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
            //DisabePlatform();
        }
    }

    private IEnumerator DisabePlatform()
    {
        this.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        this.gameObject.SetActive(true);
    }



}
