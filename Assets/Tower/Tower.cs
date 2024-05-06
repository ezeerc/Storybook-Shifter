using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Una torre que da un Shoot en área y se teletransporta(con cooldown) cuando el player se acerca

    [SerializeField] int _healthPoints;

    [SerializeField] Transform[] locations = new Transform[0];
    private int _currentLocation = 0;

    [SerializeField] Transform bulletPos;
    [SerializeField] GameObject player;
    [SerializeField] Bullet BulletPrefab;
    [SerializeField] int fireRate = 2;
    [SerializeField] float timer;
    private float distance;

    void Awake()
    {
        timer = fireRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    //teletransporte
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))//El player tiene que estar en la capa nº8
        {
            _currentLocation++;
            if (_currentLocation >= locations.Length)
            {
                _currentLocation = 0;
            }
            
            transform.position = locations[_currentLocation].position;
        }
        else return;
    }

    private void Shoot()
    {
        if (timer >= fireRate)
        {
            Bullet newBullet = Bullet.Instantiate(BulletPrefab, bulletPos.position, Quaternion.identity); 
            //newBullet.transform.up = dir;
            timer = 0;
        }

        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 10)
        {
            if (timer < fireRate)
            {
                timer += Time.deltaTime;
            }
        }
    }
    

    public void Hit(int quant)
    {
        _healthPoints -= quant;
        if (_healthPoints <= 0) Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
