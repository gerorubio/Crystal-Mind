using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionSystem {
    private List<Dice> ammunition = new List<Dice>();
    private List<Dice> currentAmmunition = new List<Dice>();

    public List<Dice> Ammunition => ammunition;
    public List<Dice> CurrentAmmunition => currentAmmunition;

    // Method to know the sum to add to spell count
    public int RemainingAmmunitionValue {
        get {
            int totalValue = 0;
            foreach(Dice dice in currentAmmunition) {
                totalValue += dice.currentFace.value;
            }
            return totalValue;
        }
    }

    public void InitializeAmmunition(int d4, int d6, int d8, int d10, int d12, int d20) {
        // D4
        for(int i = 0; i < d4; i++) {
            ammunition.Add(new Dice(4));
        }
        // D6
        for (int i = 0; i < d6; i++) {
            ammunition.Add(new Dice(6));
        }
        // D8
        for (int i = 0; i < d8; i++) {
            ammunition.Add(new Dice(8));
        }
        // D10
        for (int i = 0; i < d10; i++) {
            ammunition.Add(new Dice(10));
        }
        // D12
        for (int i = 0; i < d12; i++) {
            ammunition.Add(new Dice(12));
        }
        // D20
        for (int i = 0; i < d20; i++) {
            ammunition.Add(new Dice(20));
        }
    }

    public void Reload() {
        currentAmmunition = Shuffle();

        foreach (var dice in currentAmmunition) {
            dice.RollDice();
        }
    }

    // Fisher–Yates shuffle for Lists
    public List<Dice> Shuffle() {
        List<Dice> shuffleAmmunition = new List<Dice>(ammunition);

        int n = shuffleAmmunition.Count;
        while (n > 1) {
            int k = UnityEngine.Random.Range(0, n--);
            Dice temp = shuffleAmmunition[n];
            shuffleAmmunition[n] = shuffleAmmunition[k];
            shuffleAmmunition[k] = temp;
        }

        return shuffleAmmunition;
    }
}
