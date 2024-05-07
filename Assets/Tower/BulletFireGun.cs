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
        healthManager = FindObjectOfType<HealthManager>();
        rb.AddForce(transform.forward * 2000);
    }

    private void OnCollisionEnter(Collision col)
    {


        if(col.gameObject.tag == "Player")
        {
            Attack();
        }

    }
    public void Attack()
    {
        healthManager.TakeDamage(20);
        Destroyer(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Destroyer(int sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(this.gameObject);
    }
}
