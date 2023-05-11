using UnityEngine;

public class MouseUtils : MonoBehaviour
{
    private GameMode gameMode;

    public static Vector2 GetMousePosition2d()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Awake()
    {
        gameMode = FindObjectOfType<GameMode>();
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!gameMode.IsPause)
        {
            Cursor.visible = false;
            transform.position = GetMousePosition2d();
            transform.Rotate(0, 0, 1.5f);
        }
        else
            Cursor.visible = true;
    }

}