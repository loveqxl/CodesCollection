using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject BrainCamera;
    public GameObject MiddleVCam;
    public GameObject Player1VCam;
    public GameObject Player2VCam;

    private void Start()
    {
        
        MiddleVCam.SetActive(true);
        Player1VCam.SetActive(false);
        Player2VCam.SetActive(false);
    }

    public void MToP1() {
        MiddleVCam.SetActive(false);
        Player1VCam.SetActive(true);
    }

    public void MToP2() {
        MiddleVCam.SetActive(false);
        Player2VCam.SetActive(true);
    }

    public void P1ToM() {
        Player1VCam.SetActive(false);
        MiddleVCam.SetActive(true);
    }

    public void P2ToM() {
        Player2VCam.SetActive(false);
        MiddleVCam.SetActive(true);
    }
}
