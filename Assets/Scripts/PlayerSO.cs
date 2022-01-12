using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerSO", menuName = "ScriptableObjects/playerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    public int m_health;
    public int m_Ammo;
    public int m_maxAmmo;
    public int m_maxHealth;

    public int getHealth() { return m_health; }
    public void setHealth(int h)
    {
        if (h > m_maxHealth) { m_health = m_maxHealth; }
        else { m_health = h; }
    }
    public int getAmmo() { return m_Ammo; }
    public void setAmmo(int a)
    {
        if (a > m_maxAmmo) { m_Ammo = m_maxAmmo; }
        else { m_Ammo = a; }
    }
}
