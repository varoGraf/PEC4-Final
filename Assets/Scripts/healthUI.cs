using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthUI : MonoBehaviour
{
    public Sprite heartEmpty, heartFull;
    public PlayerSO player_info;
    private int currentHealth;
    void Start()
    {
        currentHealth = player_info.getHealth();
    }
    void Update()
    {

        currentHealth = player_info.getHealth();
        for (int i = 0; i < player_info.m_maxHealth; i++)
        {
            if (i + 1 <= currentHealth)
            {
                this.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heartFull;
            }
            else
            {
                this.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heartEmpty;
            }
        }
    }
}
