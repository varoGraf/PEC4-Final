using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject ammo;
    private bool isReady = true;
    private Vector3 positionToSpawn;


    void Start()
    {
        positionToSpawn = new Vector3(this.transform.position.x + this.GetComponent<BoxCollider2D>().bounds.size.x, this.transform.position.y, this.transform.position.z);
    }
    void Update()
    {
        if (isReady) { StartCoroutine(SpawnAmmo()); }
    }

    IEnumerator SpawnAmmo()
    {
        isReady = false;
        yield return new WaitForSeconds(1f);
        Instantiate(ammo, positionToSpawn, Quaternion.Euler(0f, 0f, 0f));
        int aux = Random.Range(0, 4);
        if (aux == 0) { yield return new WaitForSeconds(9f); }
        else if (aux == 1) { yield return new WaitForSeconds(10f); }
        else if (aux == 2) { yield return new WaitForSeconds(11f); }
        else { yield return new WaitForSeconds(12f); }
        isReady = true;
    }
}
