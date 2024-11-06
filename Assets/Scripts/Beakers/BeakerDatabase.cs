using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BeakerDatabase : ScriptableObject {
    public BeakerSO[] beaker;

    public int BeakerCount {
        get {
            return beaker.Length;
        }
    }

    public BeakerSO GetBeaker(int index) {
        return beaker[index];
    }
}
