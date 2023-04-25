using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera StandingCamera;
    [SerializeField] private CinemachineVirtualCamera FollowCamera;
    // Start is called before the first frame update
    private void Awake(){
        FollowCamera.Priority = 0;
        StandingCamera.Priority = 1;
    }

    public void SwitchPriority()
    {
        if(StandingCamera)
        {
            FollowCamera.Priority = 1;
            StandingCamera.Priority = 0;
        }
    }
}
