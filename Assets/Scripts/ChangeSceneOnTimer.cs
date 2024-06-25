using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{
    public float changeTime; // Tijd voordat scene verandert wordt
    public string sceneName;

    void Update()
    {
        changeTime -= Time.deltaTime;

        if (changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName); // Laad de opgegeven scene als de cutscene voorbij is.
        }
    }
}
