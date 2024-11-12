using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering.Universal;
using UnityEditor.Experimental;

public class UpgradeDisplay : MonoBehaviour {
    public GameObject upgradeUI;
    private GameObject parentUpgrades;

    public GameObject artifactBeakerUpgradeBox;
    public GameObject spellUpgradeBox;

    public SpellDatabase spellDatabase;
    public BeakerDatabase beakerDatabase;
    public ArtifactDatabase artifactDatabase;

    private List<UnityEngine.Object> selectedUpgrades = new List<UnityEngine.Object>();

    private Character player;
    private Weapon weapon;
    private ArtifactSO selectedArtifact;
    private BeakerSO selectedBeaker;

    // Dice upgrade
    public GameObject diceUpgradeUI;
    private GameObject parentDices;
    public GameObject diceBoxPrefab;

    private List<Dice> dices = new List<Dice>();
    private List<GameObject> dices3D = new List<GameObject>();

    public GameObject dice4Prefab, dice6Prefab, dice8Prefab, dice10Prefab, dice12Prefab, dice20Prefab;
    private Dictionary<string, GameObject> dicePrefabs = new Dictionary<string, GameObject>();

    private void Awake() {
        // Initialize the dice prefab dictionary
        dicePrefabs["d4"] = dice4Prefab;
        dicePrefabs["d6"] = dice6Prefab;
        dicePrefabs["d8"] = dice8Prefab;
        dicePrefabs["d10"] = dice10Prefab;
        dicePrefabs["d12"] = dice12Prefab;
        dicePrefabs["d20"] = dice20Prefab;
    }

    void OnEnable() {
        player = FindObjectOfType<Character>();
        weapon = FindObjectOfType<Weapon>();

        player.OnLevelUp += ShowUpgradeUI;
    }

    void OnDisable() {
        if (player != null) {
            player.OnLevelUp -= ShowUpgradeUI;
        }
    }

    private void ShowUpgradeUI(int level) {
        // Select upgrades
        SelectUpgrades();

        // Display boxes -600 -200 200 600
        DisplayUpgrades();

        // Display dices

        if (upgradeUI != null) {
            upgradeUI.SetActive(true);
        }
    }

    private void SelectUpgrades() {
        List<UnityEngine.Object> allUpgrades = new List<UnityEngine.Object>();
        //allUpgrades.AddRange(artifactDatabase.artifact);
        //allUpgrades.AddRange(spellDatabase.spell);
        allUpgrades.AddRange(beakerDatabase.beaker);

        ShuffleList(allUpgrades);

        selectedUpgrades.Clear();

        for(int i = 0; i < 4; i++) {
            if (allUpgrades.Count < 4) break;

            int index = UnityEngine.Random.Range(0, allUpgrades.Count);
            selectedUpgrades.Add(allUpgrades[index]);
            allUpgrades.RemoveAt(index); // Dont repeat upgrades in current level up
        }
    }

    private void DisplayUpgrades() {
        Vector2[] positions = new Vector2[] {
            new Vector2(-675, 0),
            new Vector2(-225, 0),
            new Vector2(225, 0),
            new Vector2(675, 0)
        };

        GameObject box;

        parentUpgrades = new GameObject();
        parentUpgrades.name = "Parent Upgrades";
        parentUpgrades.transform.parent = upgradeUI.transform;
        parentUpgrades.transform.localScale = Vector3.one;
        parentUpgrades.transform.localPosition = Vector3.zero;

        for (int i =0; i < 4; i++) {
            Debug.Log(i);
            if (selectedUpgrades[i] is ArtifactSO || selectedUpgrades[i] is BeakerSO) {
                box = Instantiate(artifactBeakerUpgradeBox, parentUpgrades.transform);
            } else {
                box = Instantiate(spellUpgradeBox, parentUpgrades.transform);
            }

            TMP_Text title = box.transform.Find("Title")?.GetComponent<TMP_Text>();
            TMP_Text effect = box.transform.Find("Effect")?.GetComponent<TMP_Text>();
            Image artwork = box.transform.Find("Artwork")?.GetComponent<Image>();
            Button button = box.GetComponent<Button>();

            // ***********   ARTIFACTS   *************
            if (selectedUpgrades[i] is ArtifactSO artifact) {
                // Title
                if (title != null) {
                    title.text = artifact.artifactName;
                }
                // Effect
                if (effect != null) {
                    effect.text = string.Join("\n\n", artifact.effects);
                }
                // Artwork
                if (artwork != null) artwork.sprite = artifact.artWork;
                // OnClick
                button.onClick.AddListener(() => {
                    selectedArtifact = artifact;
                    SelectArtifactUpgrade(artifact);
                });

            // ***********   BEAKERS   ***************
            } else if (selectedUpgrades[i] is BeakerSO beaker) {
                // Title
                if (title != null) {
                    title.text = beaker.beakerName;
                }
                // Effect
                if (effect != null) {
                    effect.text = string.Join("\n\n", beaker.description);
                }
                // Artwork
                if (artwork != null) artwork.sprite = beaker.artWork;
                // OnClick
                button.onClick.AddListener(() => {
                    selectedBeaker = beaker;
                    SelectBeakerUpgrade(beaker);
                });

            // ***********   SPELLS   ****************
            } else if (selectedUpgrades[i] is SpellSO spell) {
                // Title
                if (title != null) {
                    title.text = spell.spellName;
                }
                // Effect
                if (effect != null) {
                    effect.text = "Cost: " + spell.cost + "\n\n" + spell.description;
                }
                // Artwork
                if (artwork != null) artwork.sprite = spell.artWork;
                // OnClick
                button.onClick.AddListener(() => {
                    SelectSpellUpgrade(spell);
                });
            }

            RectTransform rectTransform = box.GetComponent<RectTransform>();
            if (rectTransform != null) {
                rectTransform.anchoredPosition = positions[i];
            }
        }
    }

    private void SelectSpellUpgrade(SpellSO spell) {
        // Replace spell
        player.CurrentSpell = spell;
        // Recalculate spell point to not be greater than spell cost
        player.CurrentSpellPoints = Mathf.Clamp(player.CurrentSpellPoints, player.CurrentSpellPoints, spell.cost);

        player.EquipSpell();

        ResumeGame();
    }

    private void SelectBeakerUpgrade(BeakerSO beaker) {
        // Beaker.Use();
        EnableDiceSelectionUI(null, beaker);
    }

    private void SelectArtifactUpgrade(ArtifactSO artifact) {

        if ((artifact.upgradeDice)) {
            EnableDiceSelectionUI(artifact, null);
        } else {
            player.EquipArtifact(artifact, null);
            ResumeGame();
        }
    }

    private void ResumeGame() {
        // Destroy and clear selected upgrades
        Destroy(parentUpgrades);
        selectedUpgrades.Clear();

        upgradeUI.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    private void EnableDiceSelectionUI(ArtifactSO artifact, BeakerSO beaker) {
        // Define positions for each dice in the UI
        Vector2[] positions = new Vector2[] {
        new Vector2(-530, 210),
        new Vector2(0, 210),
        new Vector2(530, 210),
        new Vector2(-265, -150),
        new Vector2(265, -150)
    };

        // Clear previous dice
        foreach (GameObject dice in dices3D) {
            Destroy(dice);
        }
        dices.Clear();
        dices3D.Clear();

        // Determine the number of dice selections needed
        int requiredSelections = (artifact == null) ? beaker.requiredDices : 1;

        // Create parent object for dices
        parentDices = new GameObject("DiceParent");
        parentDices.transform.SetParent(diceUpgradeUI.transform, false);

        List<Dice> ammunition = new List<Dice>(weapon.Ammunition);
        int total = Mathf.Clamp(ammunition.Count, 1, 5);

        HashSet<int> selectedIndices = new HashSet<int>();
        int selectedCount = 0; // Counter for selected dice

        // Lists to track selected dice and faces
        List<Dice> selectedDices = new List<Dice>();
        List<Face> selectedFaces = new List<Face>();

        for (int i = 0; i < total; i++) {
            // Select Dice
            int rng = UnityEngine.Random.Range(0, ammunition.Count);

            // Regenerate `rng` until it finds a unique index
            while (selectedIndices.Contains(rng)) {
                rng = UnityEngine.Random.Range(0, ammunition.Count);
            }

            selectedIndices.Add(rng);

            Dice dice = ammunition[rng];
            dices.Add(dice);

            // Create dice UI element
            GameObject diceBox = Instantiate(diceBoxPrefab, parentDices.transform);
            RectTransform diceBoxRect = diceBox.GetComponent<RectTransform>();
            if (diceBoxRect != null) {
                diceBoxRect.anchoredPosition = positions[i];
            }

            // Select 3D model to display
            int faceIndex = dice.SelectFace();

            if (dicePrefabs.ContainsKey(dice.type)) {
                GameObject newDice = Instantiate(dicePrefabs[dice.type], diceBox.transform);
                newDice.layer = LayerMask.NameToLayer("UI");
                newDice.transform.localPosition = Vector2.zero;

                Vector3 scale = dice.type.Equals("d6") ? new Vector3(1.5f, 1.5f, 1.5f) : new Vector3(1.75f, 1.75f, 1.75f);
                newDice.transform.localScale = scale;

                int value = dice.Faces[faceIndex].value;
                string effect = dice.Faces[faceIndex].effect.ToString();

                TMP_Text valueText = diceBox.transform.Find("Value")?.GetComponent<TMP_Text>();
                TMP_Text effectText = diceBox.transform.Find("Effect")?.GetComponent<TMP_Text>();

                if (valueText != null) valueText.text = value.ToString();
                if (effectText != null) effectText.text = effect;
            }

            Button button = diceBox.GetComponent<Button>();

            button.onClick.AddListener(() => {
                if (selectedCount < requiredSelections) {
                    // Apply visual effect to indicate selection
                    diceBox.GetComponent<Image>().color = Color.yellow;
                    selectedCount++;

                    // Track selected dice and face
                    selectedDices.Add(dice);
                    selectedFaces.Add(dice.Faces[faceIndex]);

                    // Verify dices selected
                    if (selectedCount == requiredSelections) {
                        if (artifact != null) {
                            Dice selectedDice = weapon.Ammunition[rng];
                            player.EquipArtifact(artifact, selectedDice.Faces[faceIndex]);
                            diceUpgradeUI.SetActive(false);
                            ResumeGame();
                            Destroy(parentDices);
                        } else {
                            // Use beaker with the selected dices and faces
                            beaker.Use(selectedDices, selectedFaces);
                            diceUpgradeUI.SetActive(false);
                            ResumeGame();
                            Destroy(parentDices);
                        }
                    }
                } else {
                    Debug.Log("Maximum selections reached.");
                }
            });
        }

        // Update UI visibility
        upgradeUI.SetActive(false);
        diceUpgradeUI.SetActive(true);
    }


    // Fisher-Yates Shuffle
    private void ShuffleList<T>(List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
