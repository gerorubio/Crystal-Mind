using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;

public class ArtifactEffectGenerator : MonoBehaviour {
    [MenuItem("Tools/Generate All Artifact Effects")]
    public static void GenerateArtifactEffects() {
        // Get the assembly containing the ArtifactEffect class (Assembly-CSharp is the default)
        var assembly = Assembly.GetAssembly(typeof(ArtifactEffect));

        // Get all types that are subclasses of ArtifactEffect
        var effectTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ArtifactEffect)) && !t.IsAbstract)
            .ToArray();

        if (effectTypes.Length == 0) {
            Debug.LogWarning("No artifact effects found.");
        }

        foreach (var effectType in effectTypes) {
            // Check the type name
            Debug.Log("Found type: " + effectType.FullName);

            var effectInstance = ScriptableObject.CreateInstance(effectType);
            string path = $"Assets/Scripts/Artifacts/Effects/{effectType.Name}.asset";

            // Create the asset at the specified path
            AssetDatabase.CreateAsset(effectInstance, path);

            Debug.Log($"Created: {path}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("All artifact effects created");
    }
}
