using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionDisplay : MonoBehaviour {
    // Databases
    public ArtifactDatabase artifactDatabase;
    public SpellDatabase spellDatabase;
    public BeakerDatabase beakerDatabase;

    // UI prefab
    public GameObject boxContainer;
    public GameObject circleContainer;

    // Containers
    public GameObject artifactContainer;
    public GameObject spellContainer;
    public GameObject beakerContainer;

    void Start() {
        ArtifactDisplay();
        SpellDisplay();
        BeakerDisplay();
    }

    private void ArtifactDisplay() {
        int i = 0;
        int j = 0;

        float xOffset = 260f; // Horizontal spacing between artifacts
        float yOffset = 280f; // Vertical spacing between artifacts

        // Get the height of the parent container to start from the top-left
        RectTransform parentRect = artifactContainer.GetComponent<RectTransform>();
        float startX = 40; // Start at the left edge
        float startY = -50; // Start at the top edge

        foreach (var artifact in artifactDatabase.artifact) {
            GameObject artifactPrefab = Instantiate(boxContainer, artifactContainer.transform);

            RectTransform rectTransform = artifactPrefab.GetComponent<RectTransform>();

            // Calculate position relative to the top-left corner
            float xPos = startX + (i * xOffset);
            float yPos = startY - (j * yOffset);
            rectTransform.localPosition = new Vector2(xPos, yPos);

            Image parentImage = artifactPrefab.GetComponent<Image>();
            if (parentImage != null) {
                parentImage.sprite = artifact.artWork;
            }

            i++;

            if (i > 4) {
                i = 0;
                j++;
            }
        }
    }

    private void SpellDisplay() {
        int i = 0;
        int j = 0;

        float xOffset = 260f; // Horizontal spacing between artifacts
        float yOffset = 280f; // Vertical spacing between artifacts

        // Get the height of the parent container to start from the top-left
        RectTransform parentRect = spellContainer.GetComponent<RectTransform>();
        float startX = 40; // Start at the left edge
        float startY = -50; // Start at the top edge

        foreach (var spell in spellDatabase.spell) {
            GameObject spellPrefab = Instantiate(circleContainer, spellContainer.transform);

            RectTransform rectTransform = spellPrefab.GetComponent<RectTransform>();

            // Calculate position relative to the top-left corner
            float xPos = startX + (i * xOffset);
            float yPos = startY - (j * yOffset);
            rectTransform.localPosition = new Vector2(xPos, yPos);

            Image parentImage = spellPrefab.GetComponent<Image>();
            if (parentImage != null) {
                parentImage.sprite = spell.artWork;
            }

            i++;

            if (i > 4) {
                i = 0;
                j++;
            }
        }
    }

    private void BeakerDisplay() {
        int i = 0;
        int j = 0;

        float xOffset = 260f; // Horizontal spacing between artifacts
        float yOffset = 280f; // Vertical spacing between artifacts

        // Get the height of the parent container to start from the top-left
        RectTransform parentRect = beakerContainer.GetComponent<RectTransform>();
        float startX = 40; // Start at the left edge
        float startY = -50; // Start at the top edge

        foreach (var beaker in beakerDatabase.beaker) {
            GameObject beakerPrefab = Instantiate(boxContainer, beakerContainer.transform);

            RectTransform rectTransform = beakerPrefab.GetComponent<RectTransform>();

            // Calculate position relative to the top-left corner
            float xPos = startX + (i * xOffset);
            float yPos = startY - (j * yOffset);
            rectTransform.localPosition = new Vector2(xPos, yPos);

            Image parentImage = beakerPrefab.GetComponent<Image>();
            if (parentImage != null) {
                parentImage.sprite = beaker.artWork;
            }

            i++;

            if (i > 4) {
                i = 0;
                j++;
            }
        }
    }
}
