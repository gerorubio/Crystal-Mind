using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceDisplay : MonoBehaviour {
    public Transform diceParent;
    public GameObject dice4Prefab, dice6Prefab, dice8Prefab, dice10Prefab, dice12Prefab, dice20Prefab;

    private Dictionary<string, GameObject> dicePrefabs = new Dictionary<string, GameObject>();
    private List<GameObject> displayedDice = new List<GameObject>();
    public TMP_Text addictionalDiceText;

    private bool isRotating = false;

    void Start() {
        // Initialize the dice prefab dictionary
        dicePrefabs["d4"] = dice4Prefab;
        dicePrefabs["d6"] = dice6Prefab;
        dicePrefabs["d8"] = dice8Prefab;
        dicePrefabs["d10"] = dice10Prefab;
        dicePrefabs["d12"] = dice12Prefab;
        dicePrefabs["d20"] = dice20Prefab;
    }

    public void UpdateDiceDisplay(List<Dice> currentAmmunition) {
        // Destroy previously displayed dice
        foreach (var dice in displayedDice) {
            Destroy(dice);
        }
        // Clear the list
        displayedDice.Clear();

        float spacing = 100f;
        Vector3 startPosition;

        if (currentAmmunition.Count >= 2) {
            startPosition = new Vector3(Mathf.Max(-50 * (currentAmmunition.Count - 1), -250), 0, 0);
        } else {
            startPosition = Vector3.zero;
        }

        int i = 0;
        int maxIterations = Mathf.Min(currentAmmunition.Count, 6);

        while (i < maxIterations) {
            var dice = currentAmmunition[i];
            if (dicePrefabs.ContainsKey(dice.type)) {
                GameObject newDice = Instantiate(dicePrefabs[dice.type], diceParent);
                newDice.transform.parent = diceParent;
                newDice.layer = LayerMask.NameToLayer("UI");

                Vector3 positionOffset = new Vector3(i * spacing, 0, 0);
                newDice.transform.localPosition = startPosition + positionOffset;

                displayedDice.Add(newDice);
            }
            i++;
        }

        if (currentAmmunition.Count > 6) {
            addictionalDiceText.text = "+" + (currentAmmunition.Count - 6).ToString();
        } else {
            addictionalDiceText.text = "";
        }
    }

    public void StartRotatingDice() {
        isRotating = true;
        StartCoroutine(RotateDice());
    }

    public void StopRotatingDice() {
        isRotating = false;
    }

    private IEnumerator RotateDice() {
        while (isRotating) {
            foreach (var dice in displayedDice) {
                // Random Vector 3 for rotating

                Vector3 randomVector = new Vector3(
                    Random.Range(-20f, 20f),
                    Random.Range(-20f, 20f),
                    Random.Range(-20f, 20f)
                );

                dice.transform.Rotate(randomVector * 1000 * Time.deltaTime);
            }
            yield return null;
        }
    }
}
