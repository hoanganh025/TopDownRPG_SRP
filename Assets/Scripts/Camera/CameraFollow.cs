using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //singleton
    public static CameraFollow cameraFollow;
    private CinemachineVirtualCamera CinemachineVirtualCamera;

    private void Awake()
    {
        if (CinemachineVirtualCamera == null)
        {
            cameraFollow = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        //find game object have tag "player" and set virtual camera follow with it
        CinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

}
