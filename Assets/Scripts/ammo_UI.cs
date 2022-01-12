using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammo_UI : MonoBehaviour
{
    public PlayerSO player_info;
    private Text text;
    private string textOutput;

    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
        textOutput = player_info.getAmmo().ToString() + "/10";
        text.text = textOutput;
    }

    void Update()
    {
        textOutput = player_info.getAmmo().ToString() + "/10";
        text.text = textOutput;
    }
}
