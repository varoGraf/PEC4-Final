using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsWithTime : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(autoDestruct());
    }


    IEnumerator autoDestruct()
    {
        Color previous = this.transform.GetComponent<SpriteRenderer>().color;
        yield return new WaitForSeconds(6f);
        if (this.tag == "HeartContainer")
        {
            this.transform.GetComponent<SpriteRenderer>().color = new Color(0.47f, 0.73f, 1f, 1f);
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 0.53f, 0.88f, 1f);
        }
        yield return new WaitForSeconds(3f);
        this.transform.parent = null;
        Destroy(this.gameObject);
    }
}
