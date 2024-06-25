using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enterDoor : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controleert of de speler of de piraat de trigger binnengaat en niet zelf een trigger is
        if (other.CompareTag("Player") || (other.CompareTag("PirateDungeon") && !other.isTrigger))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
