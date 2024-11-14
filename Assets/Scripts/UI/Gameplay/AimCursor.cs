using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Device;

public class AimCursor : MonoBehaviour {
    public Texture2D aim;

    PlayerController controller;

    void Start() {
        Vector2 hotspot = new Vector2 (aim.width / 2, aim.height / 2);
        Cursor.SetCursor(aim, hotspot, CursorMode.ForceSoftware);

        controller = GetComponent<PlayerController>();

        controller.OnAutoAim += AutoAim;
    }

    private void AutoAim(Vector3 screenPosition) {
        int screenX = (int)screenPosition.x;
        int screenY = UnityEngine.Screen.height - (int)screenPosition.y; // Invert Y-axis for screen coordinates

        // Set cursor to enemy position on screen
        SetCursorPos(screenX, screenY);
    }


    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
}
