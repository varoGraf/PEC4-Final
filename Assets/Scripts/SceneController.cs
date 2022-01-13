using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Texture2D cursor;
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}