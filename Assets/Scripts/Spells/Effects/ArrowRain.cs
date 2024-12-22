using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrowRain", menuName = "Spells/Effects/ArrowRainEffect")]
public class ArrowRain : SpellEffect {
    public GameObject arrowRain;

    public override void Cast(Character player) {
        Instantiate(arrowRain, player.transform.position, Quaternion.identity);
    }
}