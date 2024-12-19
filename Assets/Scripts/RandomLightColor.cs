using UnityEngine;

public class RandomLightColor : MonoBehaviour {
    private Light pointLight;
    public float changeInterval = 2f;
    public float lerpSpeed = 1f;

    private Color currentColor;
    private Color targetColor;
    private float timer = 0f;

    void Start() {
        pointLight = GetComponent<Light>();
        if (pointLight == null) {
            Debug.LogError($"No Light component found on {gameObject.name}. Please attach a Light component.");
            enabled = false;
            return;
        }
        currentColor = pointLight.color;
        targetColor = GetRandomColor();
    }

    void Update() {
        timer += Time.deltaTime;
        pointLight.color = Color.Lerp(pointLight.color, targetColor, lerpSpeed * Time.deltaTime);
        if (timer >= changeInterval) {
            targetColor = GetRandomColor();
            timer = 0f;
        }
    }

    private Color GetRandomColor() {
        return new Color(Random.value, Random.value, Random.value);
    }
}
