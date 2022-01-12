using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float m_speed = 30f;
    void Start()
    {

    }

    void Update()
    {
        this.transform.position += this.transform.right * m_speed * Time.deltaTime;
        StartCoroutine(autoDestruct());
    }

    IEnumerator autoDestruct()
    {
        yield return new WaitForSeconds(4f);
        this.transform.parent = null;
        Destroy(this.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }
}
