using UnityEngine;

public class Crosshair : MonoBehaviour
{

    void Update()
    {

        this.transform.position = Input.mousePosition;
    }
}
