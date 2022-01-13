using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform Player;
    public SettingsSO m_settings;
    public PlayerSO m_player;
    public GameObject[] spawnPoints;
    public GameObject zombie1, zombie2, zombie3;
    public GameObject pauseMenu;

    private bool perNumEnemies = false;
    private bool perTime = false;
    private float oneSec = 0f;
    private bool spawnActive = false;
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
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


        if (m_player.m_health <= 0)
        {
            pauseMenu.SetActive(true);
            pauseMenu.transform.GetChild(0).GetComponent<Text>().text = "You lose!";
            Time.timeScale = 0;
        }

        //Gamemode -> Number of enemies killed
        if (perNumEnemies)
        {
            if (m_settings.numEnemiesKilled >= m_settings.enemiesToKill[m_settings.getDifficulty()])
            {
                pauseMenu.SetActive(true);
                pauseMenu.transform.GetChild(0).GetComponent<Text>().text = "You win!";
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
                pauseMenu.transform.GetChild(0).GetComponent<Text>().text = "You win!";
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }

        }


        //Code for spawning enemies
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
            if (auxRand < 13) { Instantiate(zombie1, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            else if (auxRand < 18) { Instantiate(zombie2, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            else { Instantiate(zombie3, spawnPoint.transform.position, Quaternion.Euler(0f, 0f, 0f)); }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
