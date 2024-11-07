using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour {
    public GameObject upgradeUI;

    public GameObject artifactBeakerUpgradeBox;
    public GameObject spellUpgradeBox;

    public SpellDatabase spellDatabase;
    public BeakerDatabase beakerDatabase;
    public ArtifactDatabase artifactDatabase;

    public List<Object> selectedUpgrades = new List<Object>();

    Character player;
    Weapon weapon;

    void OnEnable() {
        player = FindObjectOfType<Character>();
        weapon = FindObjectOfType<Weapon>();
        player.OnLevelUp += ShowUpgradeUI;

        // Select upgrades
        SelectUpgrades();

        // Display boxes -600 -200 200 600
        DisplayUpgrades();

        // Display dices

    }

    void OnDisable() {
        if (player != null) {
            player.OnLevelUp -= ShowUpgradeUI;
        }
    }

    private void ShowUpgradeUI(int level) {
        if(upgradeUI != null) {
            upgradeUI.SetActive(true);
        }
    }

    private void SelectUpgrades() {
        List<Object> allUpgrades = new List<Object>();
        //allUpgrades.AddRange(artifactDatabase.artifact);
        allUpgrades.AddRange(spellDatabase.spell);
        //allUpgrades.AddRange(beakerDatabase.beaker);

        ShuffleList(allUpgrades);

        selectedUpgrades.Clear();

        for(int i = 0; i < 4; i++) {
            if (allUpgrades.Count < 4) break;

            int index = Random.Range(0, allUpgrades.Count);
            selectedUpgrades.Add(allUpgrades[index]);
            allUpgrades.RemoveAt(index); // Dont repeat upgrades in current level up
        }
    }

    private void DisplayUpgrades() {
        Vector2[] positions = new Vector2[] {
            new Vector2(-600, 90),
            new Vector2(-200, 90),
            new Vector2(200, 90),
            new Vector2(600, 90)
        };

        GameObject box;

        for(int i =0; i < 4; i++) {
            if (selectedUpgrades[i] is ArtifactSO || selectedUpgrades[i] is BeakerSO) {
                box = Instantiate(artifactBeakerUpgradeBox, upgradeUI.transform);
            } else {
                box = Instantiate(spellUpgradeBox, upgradeUI.transform);
            }

            TMP_Text effect = box.transform.Find("Effect")?.GetComponent<TMP_Text>();
            Image artwork = box.transform.Find("Artwork")?.GetComponent<Image>();
            Button button = box.GetComponent<Button>();

            if (selectedUpgrades[i] is ArtifactSO artifact) {
                if (effect != null) effect.text = string.Join("\n\n", artifact.effects);
                if (artwork != null) artwork.sprite = artifact.artWork;
                //button.onClick.AddListener(() => artifact.Equip(player, weapon, null));
            } else if (selectedUpgrades[i] is BeakerSO beaker) {
                if (effect != null) effect.text = beaker.description;
                if (artwork != null) artwork.sprite = beaker.artWork;
                //button.onClick.AddListener(() => beaker.Use());
            } else if (selectedUpgrades[i] is SpellSO spell) {
                if (effect != null) effect.text = spell.description;
                if (artwork != null) artwork.sprite = spell.artWork;
                button.onClick.AddListener(() => {
                    player.CurrentSpell = spell;
                    upgradeUI.SetActive(false);
                    GameManager.Instance.ResumeGame();
                });
            }

            RectTransform rectTransform = box.GetComponent<RectTransform>();
            if (rectTransform != null) {
                rectTransform.anchoredPosition = positions[i];
            }
        }
    }

    // Fisher-Yates Shuffle
    private void ShuffleList<T>(List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
