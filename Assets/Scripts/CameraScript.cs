using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Player;
    void Start()
    {
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);

    }

    void Update()
    {
        Camera.main.transform.position = new Vector3(Player.position.x, Player.position.y, Camera.main.transform.position.z);

    }
}
