using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    private ParticleSystem m_particleSystem;
    private bool isSoundPlaying = false;
    private Transform m_target;
    private int m_health;

    public EnemySO m_enemySO;
    public AudioClip audioClip1, audioClip2, audioClip3;
    public AudioSource m_audioSource;

    void Start()
    {
        m_health = m_enemySO.m_maxHealth;
        m_spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_enemySO.getSprite();
        m_spriteRenderer.color = m_enemySO.getColor();
        m_particleSystem = this.transform.GetComponentInChildren<ParticleSystem>();
        m_particleSystem.Play();
        m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (!isSoundPlaying) { StartCoroutine(playSound()); }
    }

    void FixedUpdate()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, m_target.position, m_enemySO.m_speed * Time.deltaTime);
    }

    IEnumerator playSound()
    {
        isSoundPlaying = true;
        int auxSound = Random.Range(0, 5);
        float clipLength = 0f;
        if (auxSound == 0) { m_audioSource.PlayOneShot(audioClip1); clipLength = audioClip1.length; }
        else if (auxSound == 1) { m_audioSource.PlayOneShot(audioClip2); clipLength = audioClip2.length; }
        else if (auxSound == 2) { m_audioSource.PlayOneShot(audioClip3); clipLength = audioClip3.length; }

        int auxRand = Random.Range(0, 4);
        if (auxRand == 0) { yield return new WaitForSeconds(1f + clipLength); }
        else if (auxRand == 1) { yield return new WaitForSeconds(2f + clipLength); }
        else if (auxRand == 2) { yield return new WaitForSeconds(2.5f + clipLength); }
        else { yield return new WaitForSeconds(1.5f + clipLength); }
        isSoundPlaying = false;
    }
}
