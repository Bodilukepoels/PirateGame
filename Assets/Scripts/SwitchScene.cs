using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void GoToSceneGameOne() {
        SceneManager.LoadScene("CutsceneBegin");
    }

    public void GoToSceneMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
