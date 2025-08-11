using UnityEngine;

public class ToggleBuyMenu : MonoBehaviour
{
    private static ToggleBuyMenu instance;

    public Canvas buyMenuCanvas;
    public AudioSource audioSource;

    private void Awake() //DontDestroyOnLoad
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Zoekt de BuyMenuCanvas en audiosource als die er niet zijn
        if (buyMenuCanvas == null)
        {
            buyMenuCanvas = GameObject.Find("BuyMenuCanvas").GetComponent<Canvas>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Zet de buyMenuCanvas aan of uit
    public void ToggleCanvas()
    {
            buyMenuCanvas.enabled = !buyMenuCanvas.enabled;
    }

    // Maak een apen geluid als je de trigger binnenloopt
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = true;
            ToggleCanvas();
        }
    }

    // Zet het apen geluid uit als je de trigger uitloopt
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = false;
            ToggleCanvas();
        }
    }
}
