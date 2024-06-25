using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;
    public float spawnInterval = 1f;
    public Vector2 spawnAreaSize = new Vector2(20f, 3f);
    public GameObject gate;
    public AudioSource OpenGate;

    private int enemiesSpawned = 0;
    private int enemiesKilled = 0;
    private bool spawningEnabled = false;
    private Coroutine spawningCoroutine;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (!spawningEnabled)
        {
            spawningEnabled = true;
            if (spawningCoroutine != null)
            {
                StopCoroutine(spawningCoroutine);
            }
            spawningCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        enemiesSpawned = 0;
        enemiesKilled = 0;

        while (enemiesSpawned < numberOfEnemies)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
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

        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        Vector3 spawnWorldPosition = transform.position + (Vector3)spawnPosition;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnWorldPosition, Quaternion.identity);
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

        if (enemiesKilled >= numberOfEnemies)
        {
            if (gate != null)
            {
                gate.SetActive(false);
                OpenGate.Play();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}
