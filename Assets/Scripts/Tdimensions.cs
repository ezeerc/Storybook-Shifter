using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Tdimensions : MonoBehaviour
{
    Vector3 position3 = new Vector3(0, 0, 0);
    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);

    bool oneTime = false;
    bool euler = true;
    void Tdimesions()
    {
        if (PlayerController.threeDimensions)
        {
            if (!oneTime)
            {
                position2 = transform.position;
                transform.position = new Vector3(position1.x, position2.y, position2.z);
                oneTime = true;
            }
            position3.x = 0;
        }
        else
        {
            if (euler)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                if (oneTime)
                {
                    position1 = transform.position;
                    transform.position = new Vector3(0, position1.y, position1.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    oneTime = false;
                }
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            if (!euler)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (oneTime)
                {
                    position1 = transform.position;
                    transform.position = new Vector3(0, position1.y, position1.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    oneTime = false;
                }
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Tdimesions();
    }
}
