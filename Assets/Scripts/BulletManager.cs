using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float m_speed = 30f;
    public GameObject explosion;
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
    void OnTriggerEnter2D(Collider2D other)
    {
        this.transform.parent = null;
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(this.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        this.transform.parent = null;
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        Destroy(this.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }
}
