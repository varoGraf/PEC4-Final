using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Texture2D cursor;
    public Transform Player;
    public SettingsSO m_settings;
    public PlayerSO m_player;
    public GameObject[] spawnPoints;
    public GameObject zombie1, zombie2, zombie3;

    private bool perNumEnemies = false;
    private bool perTime = false;
    private float oneSec = 0f;
    private bool spawnActive = false;
    void Start()
    {
        Vector2 cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.ForceSoftware);
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);
        if (m_settings.getGameMode() == 0) { perNumEnemies = true; perTime = false; }
        else { perNumEnemies = false; perTime = true; }

        //Gamemode -> Number of enemies killed
        if (perNumEnemies)
        {
            m_settings.numEnemiesKilled = 0;
        }

        //Gamemode -> Time
        if (perTime)
        {
            m_settings.current_time = m_settings.times[m_settings.getDifficulty()];
        }
    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);


        if (m_player.m_health == 0)
        {
            //TODO: Final state losing
            Debug.Log("You Loose!");
            Time.timeScale = 0;
        }

        //Gamemode -> Number of enemies killed
        if (perNumEnemies)
        {
            if (m_settings.numEnemiesKilled >= m_settings.enemiesToKill[m_settings.getDifficulty()])
            {
                //TODO: Final state winning
                Debug.Log("You Win!");
                Time.timeScale = 0;
            }
        }
        //Gamemode -> Time
        if (perTime)
        {
            oneSec += Time.deltaTime;
            if (oneSec >= 1f)
            {
                oneSec = 0f;
                m_settings.current_time -= 1;
            }
            if (m_settings.current_time <= 0 && m_player.m_health > 0)
            {
                //TODO: Final state winning
                Debug.Log("You Win!");
                Time.timeScale = 0;
            }

        }


        //TODO: Code for spawning enemies HERE
        if (!spawnActive && Time.timeScale != 0)
        {
            StartCoroutine(spawnEnemiesGlobal(10f, 2));
        }
    }

    IEnumerator spawnEnemiesGlobal(float timeBetween, int enemiesPerSpawn)
    {
        spawnActive = true;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            StartCoroutine(spawnEnemiesLocal(spawnPoints[i], enemiesPerSpawn));
        }
        yield return new WaitForSeconds(timeBetween);
        spawnActive = false;
    }

    IEnumerator spawnEnemiesLocal(GameObject spawnPoint, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int auxRand = Random.Range(0, 20);
            if (auxRand < 12) { Instantiate(zombie1, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            else if (auxRand < 17) { Instantiate(zombie2, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            else { Instantiate(zombie3, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
