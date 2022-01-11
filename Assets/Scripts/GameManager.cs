using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D cursor;
    public Transform Player;
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);
    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);
    }
}
