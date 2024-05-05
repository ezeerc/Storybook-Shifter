using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class moveThreeD : MonoBehaviour
{

    public Vector3 position2 = new Vector3(0, 0, 0);
    public Vector3 position1 = new Vector3(0, 0, 0);
    public float position3;

    bool oneTime = false;

    void Tdimesions()
    {
        if (!PlayerController.threeDimensions)
        {

            transform.position = new Vector3(0, position2.y, position2.z);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            //oneTime = false;
        }
        else
        {
            transform.position = new Vector3(position2.x, position2.y, position2.z);
            position3 = position2.x;
            oneTime = true;
        }
    }
    void Start()
    {
        position2 = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Tdimesions();
    }
}