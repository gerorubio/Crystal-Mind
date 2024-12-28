using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    public GameObject[] enemies;
    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    private float waveDuration = 30f;

    [SerializeField]
    private GameObject betweenLevelsUI;

    private int currentWave = 1;

    // Donut shape
    [SerializeField]
    private float r = 10f;
    [SerializeField]
    private float s = 3f;

    private bool spawnEnabled = true;

    public GameObject eyeFloor;

    private bool isWaveActive = false;

    // Ending game
    public GameObject victoryScreen;
    public GameObject infiniteEyeDoor;

    public Camera mainCamera; // Reference to the main camera

    void Start() {
        StartCoroutine(GameFlow());
    }

    private IEnumerator GameFlow() {
        while (currentWave <= 7 && spawnEnabled) {
            if (currentWave == 7) {
                ReachTop(); // Call ReachTop as soon as wave 7 begins
            }

            yield return StartCoroutine(SpawnWave(currentWave));

            // Pause after waves divisible by 3 (excluding wave 7)
            if (currentWave % 3 == 0 && currentWave != 7) {
                PauseBetweenLevels();
                yield return new WaitUntil(() => !betweenLevelsUI.activeSelf); // Wait for the player to resume
            }

            currentWave++;
        }

        // After wave 7, wait for all enemies to be cleared
        if (currentWave > 7) {
            Debug.Log("Wave 7 completed. Waiting for all enemies to be cleared.");
            yield return new WaitUntil(() => !AreEnemiesRemaining());

            // Move the camera to (0, 0, 0) and make it look at the infiniteEyeDoor
            Debug.Log("All enemies cleared. Moving camera and showing victory screen.");
            MoveCameraToVictory();
        }
    }

    private IEnumerator SpawnWave(int wave) {
        isWaveActive = true;
        float waveStartTime = Time.time;

        // Spawn enemies for the duration of the wave
        while (Time.time - waveStartTime < waveDuration && spawnEnabled) {
            SpawnEnemiesForWave(wave);
            yield return new WaitForSeconds(0.5f); // Adjust spawn interval as needed
        }

        Debug.Log($"Wave {wave} completed.");
        isWaveActive = false;
    }

    private void SpawnEnemiesForWave(int wave) {
        List<GameObject> enemiesToSpawn = new List<GameObject>();

        switch (wave) {
            case 1:
                enemiesToSpawn.Add(enemies[0]);
                break;
            case 2:
                enemiesToSpawn.Add(enemies[1]);
                break;
            case 3:
                enemiesToSpawn.Add(enemies[2]);
                break;
            case 4:
                enemiesToSpawn.AddRange(new[] { enemies[0], enemies[1] });
                break;
            case 5:
                enemiesToSpawn.AddRange(new[] { enemies[0], enemies[2] });
                break;
            case 6:
                enemiesToSpawn.AddRange(new[] { enemies[1], enemies[2] });
                break;
            case 7:
                enemiesToSpawn.AddRange(enemies);
                break;
        }

        foreach (var enemy in enemiesToSpawn) {
            SpawnEnemy(enemy);
        }
    }

    private void SpawnEnemy(GameObject enemyToSpawn) {
        float angle = Random.Range(0, 2f * Mathf.PI);
        float radius = GenerateGaussian(r, s);

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        GameObject newEnemy = Instantiate(enemyToSpawn, new Vector3(x, 0, z), Quaternion.identity);
        newEnemy.transform.SetParent(enemyContainer.transform);
    }

    private bool AreEnemiesRemaining() {
        return enemyContainer.transform.childCount > 0;
    }

    public void ReachTop() {
        eyeFloor.SetActive(true);
        if (eyeFloor != null) {
            Vector3 targetPosition = new Vector3(0, 0, 46.4f);
            StartCoroutine(MoveToPosition(eyeFloor, targetPosition, 10f));
        }

        spawnEnabled = false;
    }

    private void MoveCameraToVictory() {
        StartCoroutine(CameraToPositionAndLook(new Vector3(0, 0, 0), infiniteEyeDoor.transform.position, 3f));
    }

    private IEnumerator CameraToPositionAndLook(Vector3 targetPosition, Vector3 lookAtTarget, float duration) {
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        // Add an upward tilt to the lookAtTarget
        Vector3 adjustedLookAtTarget = lookAtTarget + new Vector3(0, 2f, 0); // Adjust Y-value to look upward
        Quaternion targetRotation = Quaternion.LookRotation(adjustedLookAtTarget - targetPosition);

        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition;
        mainCamera.transform.rotation = targetRotation;

        // Display the victory screen
        if (victoryScreen != null) {
            victoryScreen.SetActive(true);
        }
        Debug.Log("Victory achieved! Camera moved with upward tilt and victory screen displayed.");
    }

    private IEnumerator MoveToPosition(GameObject obj, Vector3 targetPosition, float duration) {
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration) {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
        Debug.Log("Object reached target position!");
    }

    private float GenerateGaussian(float mean, float stdDev) {
        float u1 = Random.value;
        float u2 = Random.value;
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
