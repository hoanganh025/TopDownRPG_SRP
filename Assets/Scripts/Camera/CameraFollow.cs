using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    void Start()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
