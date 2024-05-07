using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{
    [SerializeField] Transform[] locations = new Transform[0];
    private int _currentLocation = 0;
    public GameObject bullet;
    public GameObject firePosition;

    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;

    private Transform playerTransform;

    [SerializeField] private float fireRate;
    [SerializeField] private float fireRateDelta;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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
            Shoot();
            fireRateDelta = fireRate;
        }
    }
    void Shoot()
    {
        Instantiate(bullet, firePosition.transform.position, firePosition.transform.rotation);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Me chocaron");
            _currentLocation++;
            if (_currentLocation >= locations.Length)
            {
                _currentLocation = 0;
            }

            transform.position = locations[_currentLocation].position;
        }
        else return;
    }
}
