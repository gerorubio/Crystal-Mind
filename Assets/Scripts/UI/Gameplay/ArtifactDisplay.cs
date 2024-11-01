using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactDisplay : MonoBehaviour {
    private Character character;

    public GameObject parentArtifact;
    private List<ArtifactSO> currentArtifacts = new List<ArtifactSO>();

    void Start() {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        if (character == null) {
            Debug.LogError("Player not found in scene");
        } else {
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

        // Add new artifacts to the display
        foreach (ArtifactSO artifact in currentArtifacts) {
            // Create a new UI GameObject for the artifact
            GameObject artifactUI = new GameObject();
            Image artifactIcon = artifactUI.AddComponent<Image>();

            artifactIcon.sprite = artifact.artWork;

            // Set the parent so it appears in the correct location
            RectTransform artifactTransform = artifactUI.GetComponent<RectTransform>();
            artifactTransform.SetParent(parentArtifact.transform, false); // false keeps local scale

            // Set width and height
            artifactTransform.sizeDelta = new Vector2(50, 50);
        }
    }

}