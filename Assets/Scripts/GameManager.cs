using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int roundCount = 0;
    public TMP_Text roundCountText;
    private EnemySpawner enemySpawner;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            roundCount = 0;
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
        if (scene.name == "Game 2")
        {
            roundCount++;

            enemySpawner = FindObjectOfType<EnemySpawner>();
            if (enemySpawner != null)
            {
                int baseEnemies = 10;
                int additionalEnemiesPerRound = 5;
                enemySpawner.numberOfEnemies = baseEnemies + additionalEnemiesPerRound * (roundCount - 1);
                enemySpawner.StartSpawning();
            }

            if (roundCountText != null)
            {
                roundCountText.text = $"Round: {roundCount}";
                roundCountText.gameObject.SetActive(true);
            }
        }
        else if (scene.name == "Game 1")
        {
            if (enemySpawner != null)
            {
                Destroy(enemySpawner.gameObject);
                enemySpawner = null;
            }

            if (roundCountText != null)
            {
                roundCountText.gameObject.SetActive(false);
            }
        }
        else if (scene.name == "youDied")
        {
            Destroy(gameObject);
        }
    }
}
