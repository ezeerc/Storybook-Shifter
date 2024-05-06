using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireGun : MonoBehaviour
{
    public Rigidbody rb;
    public HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        if (healthManager == null) ;
        rb = GetComponent<Rigidbody>();
        //healthManager = GetComponent<HealthManager>();
        rb.AddForce(transform.forward * 2000);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("choqué con el pj");
        /*if(col.gameObject.tag == "Player")
        {
            Attack();
        }*/

    }
    /*public void Attack()
    {

        healthManager = GetComponentInParent<HealthManager>;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
