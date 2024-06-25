using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f); 
    private float smoothTime = 0.20f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    void LateUpdate()
    {
        if (target != null)
        {
            // Zet de camera boven het hoofd van de piraat neer
            Vector3 targetPosition = target.position + offset;

            // Zorgt dat de camera soepel meebeweegt
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
