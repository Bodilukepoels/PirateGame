using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBuyMenu : MonoBehaviour
{
    private static ToggleBuyMenu instance;

    public Canvas buyMenuCanvas;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (buyMenuCanvas == null)
        {
            buyMenuCanvas = GameObject.Find("BuyMenuCanvas").GetComponent<Canvas>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

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
            audioSource.enabled = true;
            ToggleCanvas();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = false;
            ToggleCanvas();
        }
    }
}
