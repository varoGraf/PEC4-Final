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
    private bool isDead, isFading = false;
    private Animator m_anim;

    public EnemySO m_enemySO;
    public AudioClip audioClip1, audioClip2, audioClip3, damage1, damage2;
    public AudioSource m_audioSource, m_damageAudioSource;
    public GameObject BoxOfAmmo, HeartContainer;

    void Start()
    {
        m_health = m_enemySO.m_maxHealth;
        m_spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = m_enemySO.getSprite();
        m_spriteRenderer.color = m_enemySO.getColor();
        m_particleSystem = this.transform.GetComponentInChildren<ParticleSystem>();
        m_particleSystem.Play();
        m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!isSoundPlaying && !isDead) { StartCoroutine(playSound()); }
        if (m_health <= 0 && !isDead)
        {
            isDead = true;
            Destroy(this.GetComponent<Rigidbody2D>());
            Destroy(this.GetComponent<BoxCollider2D>());
            Destroy(this.transform.GetChild(0).GetComponent<BoxCollider2D>());
            m_particleSystem.Stop();
            if (Random.Range(0, 2) == 0) { m_anim.SetBool("Right", true); }
            else { m_anim.SetBool("Right", false); }
            m_anim.SetBool("Died", true);
            StartCoroutine(death());
        }
        if (isFading)
        {
            isFading = false;
            StartCoroutine(SpriteFade(m_spriteRenderer, 0f, 1f));
        }
    }

    void FixedUpdate()
    {
        if (m_health > 0)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, m_target.position, m_enemySO.m_speed * Time.deltaTime);
        }
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

    IEnumerator death()
    {
        yield return new WaitForSeconds(1f);
        isFading = true;
        int probabilityPool = Random.Range(0, 10);
        if (probabilityPool < 3)
        {
            Instantiate(BoxOfAmmo, this.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if (probabilityPool < 5)
        {
            Instantiate(HeartContainer, this.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }

        yield return new WaitForSeconds(1f);
        this.transform.parent = null;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        Destroy(this.gameObject);
    }

    public IEnumerator SpriteFade(SpriteRenderer sr, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = sr.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Bullet")
        {
            if (Random.Range(0, 2) == 0) { m_damageAudioSource.PlayOneShot(damage1); }
            else { m_damageAudioSource.PlayOneShot(damage2); }
            m_health -= 1;
        }
    }
}
