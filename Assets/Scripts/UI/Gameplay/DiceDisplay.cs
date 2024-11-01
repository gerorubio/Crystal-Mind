using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class DiceDisplay : MonoBehaviour {
    public Transform diceParent;
    // 3D models
    public GameObject dice4Prefab, dice6Prefab, dice8Prefab, dice10Prefab, dice12Prefab, dice20Prefab;

    private Dictionary<string, GameObject> dicePrefabs = new Dictionary<string, GameObject>();
    private List<GameObject> displayedDice = new List<GameObject>();

    // Additional dice beyond 6
    public TMP_Text addictionalDiceText;

    // Dice number display
    public GameObject decalPrefab;
    private List<GameObject> decals = new List<GameObject>();

    public Texture2D baseTexture; // 1 digit
    public Texture2D smallBaseTexture; // 2 digit

    private int width = 256;
    private int height = 340;

    private bool isRotating = false;

    void Awake() {
        if (diceParent == null) {
            diceParent = GameObject.Find("DiceParentContainer")?.transform;

            if (diceParent == null) {
                Debug.LogError("Dice parent could not be found.");
            }
        }

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

        // Clear decals and instantiate new ones
        ClearDecals();

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
            Dice dice = currentAmmunition[i];
            if (dicePrefabs.ContainsKey(dice.type)) {
                // 3D model of dice
                GameObject newDice = Instantiate(dicePrefabs[dice.type], diceParent);
                newDice.layer = LayerMask.NameToLayer("UI");

                Vector3 positionOffset = new Vector3(i * spacing, 0, 0);
                newDice.transform.localPosition = startPosition + positionOffset;

                displayedDice.Add(newDice);

                // Decal texture
                int diceValue = dice.currentFace.value;
                
                // Create a new Texture2D instance for each decal
                Texture2D extractedTexture = new Texture2D(width, height);

                int startX;
                Color[] pixels = new Color[width * height];

                if (diceValue >= 10) {
                    int segmentedWidth = width / 2;
                    int segmentedHeight = height;

                    int tens = diceValue / 10;
                    int units = diceValue % 10;

                    startX = tens * segmentedWidth;
                    int startX2 = units * segmentedWidth;

                    Color[] pixelsTens = smallBaseTexture.GetPixels(startX, 0, segmentedWidth, segmentedHeight);
                    Color[] pixelsUnits = smallBaseTexture.GetPixels(startX2, 0, segmentedWidth, segmentedHeight);

                    // Copy pixelTens to left side of the pixels texture
                    for (int y = 0; y < segmentedHeight; y++) {
                        for (int x = 0; x < segmentedWidth; x++) {
                            pixels[y * width + x] = pixelsTens[y * segmentedWidth + x];
                        }
                    }

                    // Copy pixelUnits to right side of the pixels texture
                    for (int y = 0; y < segmentedHeight; y++) {
                        for (int x = 0; x < segmentedWidth; x++) {
                            pixels[y * width + x + segmentedWidth] = pixelsUnits[y * segmentedWidth + x];
                        }
                    }

                } else {
                    startX = width * diceValue;

                    // Extract pixels from the base texture
                    pixels = baseTexture.GetPixels(startX, 0, width, height);
                }

                extractedTexture.SetPixels(pixels);
                extractedTexture.Apply();

                // Create a new decal instance for each dice
                GameObject newDecal = Instantiate(decalPrefab, diceParent);
                Material decalMaterial = newDecal.GetComponent<DecalProjector>().material;

                // Create a unique material instance and assign the extracted texture
                Material uniqueDecalMaterial = new Material(decalMaterial);
                uniqueDecalMaterial.SetTexture("_number", extractedTexture);
                newDecal.GetComponent<DecalProjector>().material = uniqueDecalMaterial;

                // Setting the position of the decal to be in front of the dice
                newDecal.transform.localPosition = new Vector3(newDice.transform.localPosition.x, newDice.transform.localPosition.y, -60f);

                // Store the new decal
                decals.Add(newDecal); // Add new decal to the list
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

    private void ClearDecals() {
        foreach (var decal in decals) {
            Destroy(decal); // Destroy each decal GameObject
        }
        decals.Clear(); // Clear the list after destroying
    }
}
