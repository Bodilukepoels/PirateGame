using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int roundCount = 0; // Aantal gespeelde rondes
    public TMP_Text roundCountText;
    private EnemySpawner enemySpawner;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            roundCount = 0; // Reset roundCount op start
            totalBananas.bananas = 5000;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Handel verschillende scenes af bij het laden
        if (scene.name == "Game 2")
        {
            roundCount++;
            Cursor.visible = false;

            // Zoek EnemySpawner in de huidige scene en pas het aantal enemies aan op basis van de ronde
            enemySpawner = FindObjectOfType<EnemySpawner>();
            if (enemySpawner != null)
            {
                int baseEnemies = 10;
                int additionalEnemiesPerRound = 5;
                enemySpawner.numberOfEnemies = baseEnemies + additionalEnemiesPerRound * (roundCount - 1);
                enemySpawner.StartSpawning();
            }

            // Update UI tekst voor ronde teller
            if (roundCountText != null)
            {
                roundCountText.text = $"Round: {roundCount}";
                roundCountText.gameObject.SetActive(true);
            }
        }
        else if (scene.name == "Game 1")
        {
            Cursor.visible = true;
            // Haalt de enemySpawner weg
            if (enemySpawner != null)
            {
                Destroy(enemySpawner.gameObject);
                enemySpawner = null;
            }

            // Hide de roundCount text
            if (roundCountText != null)
            {
                roundCountText.gameObject.SetActive(false);
            }
        }
        else if (scene.name == "youDied")
        {
            Cursor.visible = true;
            Destroy(gameObject); // Verwijdert GameManager bij het laden van "youDied" scene
        }
    }
}
