using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;    // Default cursor texture
    [SerializeField] private Texture2D clickCursor;      // Cursor texture for left mouse button click
    [SerializeField] private Texture2D customCursor1;    // Additional custom cursor if needed
    [SerializeField] private Texture2D customCursor2;    // Another custom cursor if needed
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;

    private void Update()
    {
        // Determine the desired cursor based on input
        Texture2D desiredCursor = GetCursorBasedOnInput();

        // Set the cursor every frame, regardless of the current cursor
        Cursor.SetCursor(desiredCursor, cursorHotspot, CursorMode.Auto);
    }

    // Determines which cursor to use based on input
    private Texture2D GetCursorBasedOnInput()
    {
        if (Input.GetMouseButton(0)) return clickCursor;

        // Additional conditions for other cursor types
        // Example: if (SomeOtherCondition) return customCursor1;

        return defaultCursor;
    }
}