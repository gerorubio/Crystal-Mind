using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject[] enemies;

    // Time between spawns
    private float spawnInterval;

    // Initial spawn interval in seconds
    [SerializeField]
    private float initialSpawnInterval = 5f;
    
    // Time for the spawn interval to halve
    [SerializeField]
    private float halfLife = 30f; 

    // Total time
    private float elapsedTime = 0f;
    // Current phase
    private int currentPhase = 0;
    // Time of each phase
    [SerializeField]
    private float[] phaseDuration = new float[4];

    // Enemy container
    private GameObject enemyContainer;
    // Donut shape variables
    [SerializeField]
    private float r = 10f;
    [SerializeField]
    private float s = 3f;

    // Reference to "Between Levels" UI
    [SerializeField]
    private GameObject betweenLevelsUI;

    private bool hasPausedAfterSecondWave = false;

    void Start() {
        enemyContainer = new GameObject("Enemy Container");
        StartCoroutine(SpawnEnemies());
    }

    private void Update() {
        elapsedTime += Time.deltaTime;

        // Update spawnInterval using exponential decay
        spawnInterval = initialSpawnInterval * Mathf.Pow(0.5f, elapsedTime / halfLife);

        // Determine current phase
        if (elapsedTime < phaseDuration[0]) {
            currentPhase = 1; // Enemy 1
        } else if (elapsedTime < phaseDuration[0] + phaseDuration[1]) {
            currentPhase = 2; // Enemy 2
        } else if (elapsedTime < phaseDuration[0] + phaseDuration[1] + phaseDuration[2]) {
            currentPhase = 3; // Enemy 1 + 3

            // Pause the game after second wave if not already paused
            if (!hasPausedAfterSecondWave) {
                PauseBetweenLevels();
                hasPausedAfterSecondWave = true;
            }

        } else {
            currentPhase = 4; // All enemies
        }
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy() {
        GameObject enemyToSpawn = null;

        switch (currentPhase) {
            case 1:
                enemyToSpawn = enemies[0];
                break;
            case 2:
                enemyToSpawn = enemies[1];
                break;
            case 3:
                enemyToSpawn = enemies[Random.Range(0, 2)];
                break;
            case 4:
                enemyToSpawn = enemies[Random.Range(0, 3)];
                break;
        }

        if (enemyToSpawn != null) {
            // Get position to spawn in a donut shape
            float a = Random.Range(0, 2f * Mathf.PI);
            float b = GenerateGaussian(r, s);

            float x = b * Mathf.Cos(a);
            float z = b * Mathf.Sin(a);

            GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(x, 0, z), Quaternion.identity);

            newEnemy.transform.SetParent(enemyContainer.transform);
        }
    }

    // Method to generate Gaussian distributed random numbers
    private float GenerateGaussian(float mean, float stdDev) {
        // Use Box-Muller Transform
        float u1 = Random.value; // Uniform(0,1) random value
        float u2 = Random.value; // Uniform(0,1) random value
        float z0 = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return z0 * stdDev + mean;
    }

    private void PauseBetweenLevels() {
        GameManager.Instance.PauseGame();
        if (betweenLevelsUI != null) {
            betweenLevelsUI.SetActive(true);
        }
    }

    public void ResumeFromBetweenLevels() {
        GameManager.Instance.ResumeGame();
        if (betweenLevelsUI != null) {
            betweenLevelsUI.SetActive(false);
        }
    }
}
