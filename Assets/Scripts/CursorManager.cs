using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D heldCursor;
    [SerializeField] Vector2 heldCursorHotspot;

    public void SetCursorDefault()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(heldCursor, Vector2.zero, CursorMode.Auto);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(defaultCursor, heldCursorHotspot, CursorMode.Auto);
        }
    }
}
