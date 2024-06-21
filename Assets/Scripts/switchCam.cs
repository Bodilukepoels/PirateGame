using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCam : MonoBehaviour
{
    public Camera cameraToActivate;
    public GameObject player;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player object is not assigned!");
        }

        if (cameraToActivate == null)
        {
            Debug.LogError("Camera to activate is not assigned!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player) ;
        {
            foreach (Camera cam in Camera.allCameras)
            {
                cam.gameObject.SetActive(false);
            }

            cameraToActivate.gameObject.SetActive(true);
        }
    }
}