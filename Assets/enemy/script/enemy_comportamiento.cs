using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_comportamiento : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacando;
    public HealthManager heartSystem;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        HealthManager heartSystem = GetComponent<HealthManager>();
    }

    public void comportamiento_enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            ani.SetBool("correr", false);
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= 4)
            {
                
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("caminar", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("caminar", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1.2)  //si el enemigo queda muy lejos, cambiar el 1.2.
            {
                var buscarPosicion = target.transform.position - transform.position;
                buscarPosicion.y = 0;
                var rotation = Quaternion.LookRotation(buscarPosicion);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("caminar", false);

                ani.SetBool("correr", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("atacar", false);
            }
            else
            if (Vector3.Distance(transform.position, target.transform.position) <= 1.2) //si el enemigo queda muy lejos, cambiar el 1.2.
            {
                ani.SetBool("caminar", false);
                ani.SetBool("correr", false);
                ani.SetBool("atacar", true);
                atacando = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        comportamiento_enemigo();
    }

    public void Attack()
    {
        heartSystem.TakeDamage(5);
    }

}
