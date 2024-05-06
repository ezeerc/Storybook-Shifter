using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{

    public float speed = 3.0f;
    [SerializeField] private Transform origin, target;
    private Vector3 firstTarget;

    private void Start()
    {
        firstTarget = target.position;
    }


    void FixedUpdate()
    {
        var step = speed * Time.deltaTime;

        if (transform.position == target.position)
        {
            target.position = origin.position;
        }

        if (transform.position == origin.position)
        {
            target.position = firstTarget;

        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);


    }


    /*private void OnTriggerEnter(Collider other)
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
            other.transform.parent = null;
            Debug.Log("Player detached from cube");
        }

    }*/
}