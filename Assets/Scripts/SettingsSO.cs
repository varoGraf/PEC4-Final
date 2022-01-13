using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "settingsSO", menuName = "ScriptableObjects/settingsSO", order = 0)]
public class SettingsSO : ScriptableObject
{
    private int m_difficulty;   //From 0 to 2, where 0 is the easiest and 2 de hardest
    private int m_gameMode;     //0 or 1, where 0 is rounds and 1 is time

    public int[] times;
    public int[] enemiesToKill;
    public int current_time = 0;
    public int numEnemiesKilled = 0;

    public void setDifficulty(int d) { m_difficulty = d; }
    public int getDifficulty() { return m_difficulty; }
    public void setGameMode(int g) { m_gameMode = g; }
    public int getGameMode() { return m_gameMode; }
}
