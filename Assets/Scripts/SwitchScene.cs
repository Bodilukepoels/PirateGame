using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Gaat naar de "CutsceneBegin" scene
    public void GoToSceneGameOne()
    {
        SceneManager.LoadScene("CutsceneBegin");
    }

    // Gaat naar de "MainMenu" scene
    public void GoToSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

