using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    private MenuController menuController;

    public string playerTag = "Player";

    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Tilemap Collider 2D component not found on the GameObject.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Als de speler de trigger raakt, roep dan TakeDamage aan in MenuController
        if (other.CompareTag(playerTag))
        {
            menuController = other.GetComponent<MenuController>();

            if (menuController != null)
            {
                menuController.TakeDamage(1000000); // Vermoord de speler
            }
            else
            {
                Debug.LogError("MenuController component not found on the player GameObject.");
            }
        }
    }
}
