using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionManager : MonoBehaviour
{
    private Animator m_anim;
    void Start()
    {
        m_anim = this.transform.GetComponent<Animator>();
        StartCoroutine(explode());
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(m_anim.GetCurrentAnimatorStateInfo(0).length);
        this.transform.parent = null;
        Destroy(this.gameObject);
    }
}
