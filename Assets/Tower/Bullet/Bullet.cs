using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float _speed = 10f;


    private void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _speed * Time.deltaTime);
    }

}

