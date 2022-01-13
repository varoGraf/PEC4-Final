using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public SettingsSO m_settings;
    public Button easyButton, normalButton, hardButton, brawlButton, timeButton;
    void Start()
    {
        m_settings.setDifficulty(1);
        m_settings.setGameMode(0);
    }

    void Update()
    {
        if (m_settings.getDifficulty() == 0)
        {
            ColorBlock cb = easyButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0.3f);
            easyButton.colors = cb;
            cb = normalButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            normalButton.colors = cb;
            cb = hardButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            hardButton.colors = cb;
        }
        else if (m_settings.getDifficulty() == 1)
        {
            ColorBlock cb = easyButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            easyButton.colors = cb;
            cb = normalButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0.3f);
            normalButton.colors = cb;
            cb = hardButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            hardButton.colors = cb;
        }
        else if (m_settings.getDifficulty() == 2)
        {
            ColorBlock cb = easyButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            easyButton.colors = cb;
            cb = normalButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            normalButton.colors = cb;
            cb = hardButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0.3f);
            hardButton.colors = cb;
        }

        if (m_settings.getGameMode() == 0)
        {
            ColorBlock cb = brawlButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0.3f);
            brawlButton.colors = cb;
            cb = timeButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            timeButton.colors = cb;
        }
        else if (m_settings.getGameMode() == 1)
        {
            ColorBlock cb = brawlButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0f);
            brawlButton.colors = cb;
            cb = timeButton.colors;
            cb.normalColor = new Color(cb.normalColor.r, cb.normalColor.g, cb.normalColor.b, 0.3f);
            timeButton.colors = cb;
        }

    }

    public void easyClicked() { m_settings.setDifficulty(0); }
    public void normalClicked() { m_settings.setDifficulty(1); }
    public void hardClicked() { m_settings.setDifficulty(2); }
    public void brawlClicked() { m_settings.setGameMode(0); }
    public void timeClicked() { m_settings.setGameMode(1); }
}
