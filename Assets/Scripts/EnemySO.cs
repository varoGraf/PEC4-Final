using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemySO", menuName = "ScriptableObjects/enemySO", order = 0)]
public class EnemySO : ScriptableObject
{
    public int m_maxHealth;
    public int m_damage;
    public Sprite m_sprite;
    public float m_speed;
    public Color m_color;


    public int getDamage() { return m_damage; }
    public float getSpeed() { return m_speed; }
    public Sprite getSprite() { return m_sprite; }
    public Color getColor() { return m_color; }
}
