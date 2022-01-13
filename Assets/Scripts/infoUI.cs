using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoUI : MonoBehaviour
{
    public GameObject infoText, countText;
    public SettingsSO m_settings;

    void Start()
    {
        if (m_settings.getGameMode() == 0)
        {
            infoText.GetComponent<Text>().text = "Enemy kill count";
        }
        else
        {
            infoText.GetComponent<Text>().text = "Time left";
        }
    }

    void Update()
    {
        if (m_settings.getGameMode() == 0)
        {
            countText.GetComponent<Text>().text = m_settings.numEnemiesKilled + "/" + m_settings.enemiesToKill[m_settings.getDifficulty()];
        }
        else
        {
            countText.GetComponent<Text>().text = m_settings.current_time.ToString();
        }
    }
}
