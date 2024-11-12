using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beaker", menuName = "Beakers")]
public class BeakerSO : ScriptableObject {
    public string beakerName;
    public string description;
    public Sprite artWork;
    public int requiredDices;

    public void Use(List<Dice> dices, List<Face> faces) {
        switch (beakerName) {
            case "Andromeda":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].value--;
                }
                break;
            case "Cetus":
                for (int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Poison;
                }
                break;
            case "Corona Borealis":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Obsidian;
                }
                break;
            case "Crux":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].value = dices[i].Faces.Length;
                }
                break;
            case "Geminis":
                faces[0].value = faces[1].value;
                faces[0].effect = faces[1].effect;
                break;
            case "Hydra":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Bleed;
                }
                break;
            case "Leo":
                faces[0].value += 3;
                break;
            case "Orion":
                faces[0].value *= 2;
                break;
            case "Pegasus":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Alceanistum;
                }
                break;
            case "Phoenix":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Burned;
                }
                break;
            case "Scorpio":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].value++;
                }
                break;
            case "Ursa Minor":
                for(int i = 0; i < faces.Count; i++) {
                    faces[i].effect = EffectType.Frozen;
                }
                break;
            default:
                break;
        }
    }
}
