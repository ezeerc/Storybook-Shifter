using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;
   //This is optional. Watch the video fore more information.


    private void Start()
    {
    }
    public float GetRateOfFire()
    {
        return rateOfFire;
    }

    public void Fire()
    {
        Instantiate(projectile, transform.position, transform.rotation);
        //you can use transform.position instead of gunPoint.position
        //if this script is attached directly to a gunpoint
    }
}