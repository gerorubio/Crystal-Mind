using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Dice {
    public string type;
    private Face[] faces;
    public Face[] Faces { get { return faces; } }

    public Face currentFace;

    public Dice(int numberOfFaces) {
        // Initialize the faces array with the specified length d4, d6, d8, d10, d12, d20
        this.faces = new Face[numberOfFaces];

        for (int i = 0; i < numberOfFaces; i++) {
            this.faces[i] = new Face(i + 1, EffectType.None);
        }

        switch (numberOfFaces) {
            case 4:
                type = "d4";
                break;
            case 6:
                type = "d6";
                break;
            case 8:
                type = "d8";
                break;
            case 10:
                type = "d10";
                break;
            case 12:
                type = "d12";
                break;
            case 20:
                type = "d20";
                break;
            default:
                Debug.LogError("Number of faces not allowed");
                break;
        }
    }

    public Dice(int numberOfFaces, float probability) {
        // Initialize the faces array with the specified length d4, d6, d8, d10, d12, d20
        this.faces = new Face[numberOfFaces];

        for (int i = 0; i < numberOfFaces; i++) {
            this.faces[i] = new Face(i + 1, EffectType.None);
            if (UnityEngine.Random.Range(0, 100) < probability) {
                // Assign a random EffectType to the face
                int effectTypeCount = Enum.GetValues(typeof(EffectType)).Length;
                this.faces[i].effect = (EffectType)UnityEngine.Random.Range(0, effectTypeCount);
            }
        }

        switch (numberOfFaces) {
            case 4:
                type = "d4";
                break;
            case 6:
                type = "d6";
                break;
            case 8:
                type = "d8";
                break;
            case 10:
                type = "d10";
                break;
            case 12:
                type = "d12";
                break;
            case 20:
                type = "d20";
                break;
            default:
                Debug.LogError("Number of faces not allowed");
                break;
        }
    }

    public Dice(int numberOfFaces, int value = 0, EffectType effectType = EffectType.None) {
        // Initialize the faces array with the specified length d4, d6, d8, d10, d12, d20
        this.faces = new Face[numberOfFaces];

        for (int i = 0; i < numberOfFaces; i++) {
            // Use default counting values and EffectType.None if value or effectType is not provided
            int faceValue = value > 0 ? value : i + 1;
            EffectType faceEffect = effectType != EffectType.None ? effectType : EffectType.None;

            // Assign values to each face
            faces[i] = new Face(faceValue, faceEffect);
        }

        switch (numberOfFaces) {
            case 4:
                type = "d4";
                break;
            case 6:
                type = "d6";
                break;
            case 8:
                type = "d8";
                break;
            case 10:
                type = "d10";
                break;
            case 12:
                type = "d12";
                break;
            case 20:
                type = "d20";
                break;
            default:
                Debug.LogError("Number of faces not allowed");
                break;
        }
    }

    public void ModifyValue(int faceIndex, int newValue) {
        if (faceIndex >= 0 && faceIndex < faces.Length) {
            this.faces[faceIndex].value = newValue;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }

    public void AddEffect(int faceIndex, EffectType newEffect) {
        if (faceIndex >= 0 && faceIndex < faces.Length) {
            this.faces[faceIndex].effect = newEffect;
        } else {
            Debug.LogError("Face index out of bounds.");
        }
    }

    public void PrintAllFaces() {
        Debug.Log("Dice Type: " + type);
        for (int i = 0; i < faces.Length; i++) {
            Debug.Log("Face " + (i + 1) + ": Value = " + faces[i].value + ", Effect = " + faces[i].effect);
        }
        Debug.Log("**************************");
    }

    public void SetToMaxFace() {
        if (faces != null && faces.Length > 0) {
            currentFace = faces.OrderByDescending(face => face.value).First();
        }
    }

    public void RollDice() {
        currentFace = this.faces[UnityEngine.Random.Range(0, this.faces.Length)];
    }

    public int SelectFace() {
        int index = UnityEngine.Random.Range(0, this.faces.Length);
        return index;
    }
}
