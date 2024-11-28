using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour {
    private Character player;
    public GameObject hpParent;
    public Sprite heart;

    void Start() {
        Debug.Log("HpDisplay Start");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

        if (player == null) {
            Debug.LogError("Player not found in scene");
        } else {
            player.OnHpChanged += UpgradeDisplay;
            Debug.Log(player.CurrentHp);
        }
    }

    private void UpgradeDisplay(int currentHp) {
        Debug.Log("HpDisplayHpDisplay");
        for (int i = 0; i < currentHp; i++) {
            GameObject heartGO = new GameObject("Heart");
            Image heartImage = heartGO.AddComponent<Image>();
            heartImage.sprite = heart;

            // Set parent
            RectTransform heartTransform = heartGO.GetComponent<RectTransform>();
            heartTransform.SetParent(hpParent.transform, false);

            // Set width and height
            heartTransform.sizeDelta = new Vector2(50, 50);

            int row = i % 5;
            int col = i / 5;

            float xPosition = row * 50;
            float yPosition = col * -50;

            heartTransform.anchoredPosition = new Vector2(xPosition, yPosition);

        }
    }
}
