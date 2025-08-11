using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCam : MonoBehaviour
{
    public Camera cameraToActivate; // De camera die wordt geactiveerd door de trigger
    public GameObject player;

    void Start()
    {
        // Controleer of de camera is toegewezen
        if (cameraToActivate == null)
        {
            Debug.LogError("Not assigned!");
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Controleer of het degene die het object heeft getriggerd een player is
        if (other.gameObject == player)
        {
            // Zet alle andere camera's uit
            foreach (Camera cam in Camera.allCameras)
            {
                cam.gameObject.SetActive(false);
                cam.gameObject.GetComponent<AudioListener>().enabled = false;
            }

            // Activeer de specifieke camera die is toegewezen
            cameraToActivate.gameObject.SetActive(true);
            cameraToActivate.gameObject.GetComponent<AudioListener>().enabled = true;
        }
    }
}
