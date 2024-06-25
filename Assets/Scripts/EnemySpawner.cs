using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;
    public float spawnInterval = 1f;
    public Vector2 spawnAreaSize = new Vector2(20f, 3f); // Grootte van het gebied waarin enemies kunnen spawnen
    public GameObject gate;
    public AudioSource OpenGate;

    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;
    private bool spawningEnabled = false;
    private Coroutine spawningCoroutine; // Coroutine die het spawnen van enemies beheert

    void Start()
    {
        // Begin direct met spawnen van enemies bij het starten van het spel
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!spawningEnabled)
        {
            spawningEnabled = true;
            if (spawningCoroutine != null)
            {
                // Stop de coroutine als die al actief is
                StopCoroutine(spawningCoroutine);
            }
            spawningCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        // Reset het aantal gespawnde enemies en gedode enemies
        enemiesSpawned = 0; 
        enemiesKilled = 0;

        while (enemiesSpawned < numberOfEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wacht voor het volgende spawn
        }

        Debug.Log("Spawning finished!");
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Not Assigned!");
            return;
        }

        // Bepaal een random spawnposition binnen het opgegeven gebied
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        Vector3 spawnWorldPosition = transform.position + (Vector3)spawnPosition;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnWorldPosition, Quaternion.identity);

        // Geeft een referentie naar de spawner mee als die bestaat
        EnemyFollow enemyFollow = newEnemy.GetComponent<EnemyFollow>();
        if (enemyFollow != null)
        {
            enemyFollow.SetSpawnerReference(this);
        }

        enemiesSpawned++;
    }

    public void EnemyDied()
    {
        enemiesKilled++;

        Debug.Log($"Enemies Killed: {enemiesKilled} - {numberOfEnemies}");

        // Controleert of alle enemies dood zijn
        if (enemiesKilled >= numberOfEnemies)
        {
                // Schakelt de poort uit
                gate.SetActive(false);
                OpenGate.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Tekent een rode kubus om aan te geven waar de enemies kunnen spawnen
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}
