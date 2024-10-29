using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Make sure this namespace is included

public class PruebaDecal : MonoBehaviour {
    public Texture2D baseTexture;

    public int x1 = 0;
    public int y1 = 0;
    public int x2 = 256;
    public int y2 = 340;

    private Texture2D extractedTexture;
    public GameObject decalPrefab;

    void Update() {
        int width = x2 - x1;
        int height = y2 - y1;

        // Create a new texture with the specified dimensions
        extractedTexture = new Texture2D(width, height);

        // Copy the pixels from the base texture
        Color[] pixels = baseTexture.GetPixels(x1, y1, width, height);
        extractedTexture.SetPixels(pixels);
        extractedTexture.Apply();

        // Get the DecalProjector component
        DecalProjector decal = decalPrefab.GetComponent<DecalProjector>();
        
        if (decal == null) {
            Debug.LogError("Decal Projector component not found on the prefab.");
            return;
        }

        Material decalMaterial = decal.material;

        if (decalMaterial != null) {
            decalMaterial.SetTexture("_number", extractedTexture);
        } else {
            Debug.LogWarning("Decal Projector's material is not set.");
        }
    }
}
