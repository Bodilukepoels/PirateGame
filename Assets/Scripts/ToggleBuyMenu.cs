using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBuyMenu : MonoBehaviour
{
    public Canvas buyMenuCanvas;
    public AudioSource Audio;

    public void ToggleCanvas()
    {
        if (buyMenuCanvas != null)
        {
            buyMenuCanvas.enabled = !buyMenuCanvas.enabled;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Audio.enabled = true;
            ToggleCanvas();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Audio.enabled = false;
            ToggleCanvas();
        }
    }
}
