using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crosshair : MonoBehaviour
{
    void Start()
    {
        // Controleert of de Scene Game 2 is en hide dan de cursor, anders niet
        if (SceneManager.GetActiveScene().name == "Game 2")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    void Update()
    {
        // Verplaatst de crosshair naar de positie van de muis
        this.transform.position = Input.mousePosition;
    }
}
