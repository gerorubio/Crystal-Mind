using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Color in text
//TMP_TextInfo

public class ArtifactDisplay : MonoBehaviour {
    private Character character;

    public GameObject parentArtifact;
    private List<ArtifactSO> currentArtifacts = new List<ArtifactSO>();

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        if (character == null) {
            Debug.LogError("Player not found in scene");
        } else {
            AddArtifact(character.CurrentArtifacts.First());
            character.OnEquipArtifact += AddArtifact;
        }
    }

    private void AddArtifact(ArtifactSO artifact) {
        currentArtifacts.Add(artifact);

        UpdateDisplay();
    }

    private void UpdateDisplay() {
        // Clear previous artifacts
        foreach (Transform artifact in parentArtifact.transform) {
            Destroy(artifact.gameObject);
        }

        float verticalSpacing = 10f; // Vertically space
        float horizontalOffset = 40f; // Space for second column
        int maxArtifactsPerColumn = 20; // Number of artifacts per column

        for (int i = 0; i < currentArtifacts.Count; i++) {
            ArtifactSO artifact = currentArtifacts[i];

            GameObject artifactUI = new GameObject(artifact.name);
            Image artifactIcon = artifactUI.AddComponent<Image>();
            artifactIcon.sprite = artifact.artWork;

            // Set parent
            RectTransform artifactTransform = artifactUI.GetComponent<RectTransform>();
            artifactTransform.SetParent(parentArtifact.transform, false);

            // Set width and height
            artifactTransform.sizeDelta = new Vector2(40, 40);

            int row = i / maxArtifactsPerColumn;
            int column = i % maxArtifactsPerColumn;

            float xPosition = column * horizontalOffset;
            float yPosition = -row * (30 + verticalSpacing);

            artifactTransform.anchoredPosition = new Vector2(xPosition, yPosition);
        }
    }
}