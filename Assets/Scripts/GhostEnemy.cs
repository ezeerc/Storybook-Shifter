using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float range;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < range)
        {
            Vector3 direction = player.position - transform.position;

            if(player.position.x < transform.position.x) //left
            {
                if(player.localScale.x == -1)
                {
                    Chase();
                }
            }

            if (player.position.x > transform.position.x) //right
            {
                if (player.localScale.x == 1)
                {
                    Chase();
                }
            }

        }
    }

    void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}
