using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;

    private Transform playerTransform;
    private Gun currentGun;
    private float fireRate;
    private float fireRateDelta;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentGun = GetComponentInChildren<Gun>();
        fireRate = currentGun.GetRateOfFire();
    }

    private void Update()
    {
        Vector3 playerGroundPos = new Vector3(playerTransform.position.x,
                                  transform.position.y, playerTransform.position.z);

        if (Vector3.Distance(transform.position, playerGroundPos) > turretRange)
        {
            return;
        }

        Vector3 playerDirection = playerGroundPos - transform.position;
        float turretRotationStep = turretRotationSpeed * Time.deltaTime;
        Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, playerDirection,
                                   turretRotationStep, 0f);
        transform.rotation = Quaternion.LookRotation(newLookDirection);

        fireRateDelta -= Time.deltaTime;
        if (fireRateDelta <= 0)
        {
            currentGun.Fire();
            fireRateDelta = fireRate;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange); //Show a gizmo when selected
    }
}