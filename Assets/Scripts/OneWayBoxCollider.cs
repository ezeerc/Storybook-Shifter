using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class OneWayBoxCollider : MonoBehaviour
{
    [SerializeField] private Vector3 entryDirection= Vector3.up;
    [SerializeField] private bool localDirection = false;
    [SerializeField] private float triggerScale = 2f;
    private new BoxCollider collider = null;

    private BoxCollider collisionCheckTrigger = null;

    private void Awake()
    {
        collider= GetComponent<BoxCollider>();
        collider.isTrigger= false;
        collisionCheckTrigger = gameObject.AddComponent<BoxCollider>();
        collisionCheckTrigger.size = collider.size * triggerScale;
        collisionCheckTrigger.center = collider.center;
        collisionCheckTrigger.isTrigger= true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Physics.ComputePenetration(collisionCheckTrigger, transform.position, transform.rotation,
            other, other.transform.position, other.transform.rotation,
            out Vector3 collisionDirection, out float penetrationDepth
            ))
        {
            Vector3 direction = default;

            if (localDirection)
            {
                direction = transform.TransformDirection(entryDirection.normalized);
            }

            else
            {
                direction = entryDirection;
            }

            float dot = Vector3.Dot(entryDirection, collisionDirection);
            if(dot < 0) 
            {
                Physics.IgnoreCollision(collider, other, false);
            }
            else
            {
                Physics.IgnoreCollision(collider, other, true);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 direction = default;

        if (localDirection)
        {
            direction = transform.TransformDirection(entryDirection.normalized);
        }

        else
        {
            direction = entryDirection;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);


        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, -direction);
    }
}
