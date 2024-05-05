using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam= this.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (vcam.Priority == 1)
            {
                vcam.Priority = 10;
                PlayerController.threeDimensions = true;
                Camera.main.orthographic = false;

            }
            else
            {
                vcam.Priority = 1;
                PlayerController.threeDimensions = false;
                Camera.main.orthographic = true;
            }
        }

    }
}