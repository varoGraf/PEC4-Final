using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D m_rigidBody2D;
    private Transform m_weapon;
    private float m_weaponAngle;
    private ParticleSystem dust;
    private bool isShooting = false;
    private bool isSteping = false;
    private AudioSource audioSourceGunshot, audioSourceSteps, damageAudioSource;
    private bool isDead = false;
    private bool isHit = false;


    public PlayerSO m_playerSO;
    public float m_speed = 4f;
    public GameObject bulletFire;
    public GameObject bullet;
    public AudioClip step1, step2, gunshot, noAmmo, damage1, damage2, heartPickUp, ammoPickUp;


    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        m_rigidBody2D = this.GetComponent<Rigidbody2D>();
        m_weapon = this.transform.GetChild(1);
        dust = this.transform.GetChild(2).GetComponent<ParticleSystem>();
        var emission = dust.emission;
        emission.enabled = false;
        m_weaponAngle = 0;
        bulletFire.SetActive(false);
        audioSourceGunshot = this.GetComponents<AudioSource>()[0];
        audioSourceSteps = this.GetComponents<AudioSource>()[1];
        damageAudioSource = this.GetComponents<AudioSource>()[2];
        m_playerSO.setAmmo(m_playerSO.m_maxAmmo / 2);
        m_playerSO.setHealth(m_playerSO.m_maxHealth);
    }

    void Update()
    {

        if (m_playerSO.m_health <= 0)
        {
            isDead = true;
        }

        if (Input.GetButtonDown("Fire1") && !isDead)
        {
            if (!isShooting)
            {
                if (m_playerSO.getAmmo() > 0)
                {
                    audioSourceGunshot.PlayOneShot(gunshot);
                    StartCoroutine((fireGun()));
                    m_playerSO.setAmmo(m_playerSO.getAmmo() - 1);
                }
                else
                {
                    audioSourceGunshot.PlayOneShot(noAmmo);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            var emission = dust.emission;
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y + GetComponent<BoxCollider2D>().bounds.size.y / 2, transform.position.z);
            difference.Normalize();

            float rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
            Vector3 playerPosition = new Vector3(this.transform.position.x, this.transform.position.y + GetComponent<BoxCollider2D>().bounds.size.y / 2, this.transform.position.z);

            m_weapon.RotateAround(playerPosition, new Vector3(0, 0, 1), rotationZ - m_weaponAngle);
            m_weaponAngle = rotationZ;
            if (m_weaponAngle > 90 || m_weaponAngle < -90)
            {
                m_weapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                m_weapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }

            if (Input.GetButton("Horizontal"))
            {
                emission.enabled = true;
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    animator.SetBool("isRunning", true);
                    m_rigidBody2D.velocity = new Vector2(m_speed, m_rigidBody2D.velocity.y);
                }
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                    animator.SetBool("isRunning", true);
                    m_rigidBody2D.velocity = new Vector2(-m_speed, m_rigidBody2D.velocity.y);
                }
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    emission.enabled = false;
                    if (m_rigidBody2D.velocity.y != 0) { animator.SetBool("isRunning", true); }
                    else { animator.SetBool("isRunning", false); }
                    m_rigidBody2D.velocity = new Vector2(0, m_rigidBody2D.velocity.y);
                }
            }
            else
            {
                m_rigidBody2D.velocity = new Vector2(0, m_rigidBody2D.velocity.y);
            }
            if (Input.GetButton("Vertical"))
            {
                emission.enabled = true;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    animator.SetBool("isRunning", true);
                    m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, m_speed);
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    animator.SetBool("isRunning", true);
                    m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, -m_speed);
                }
                if (Input.GetAxisRaw("Vertical") == 0)
                {
                    emission.enabled = false;
                    if (m_rigidBody2D.velocity.x != 0) { animator.SetBool("isRunning", true); }
                    else { animator.SetBool("isRunning", false); }
                    m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, 0);
                }
            }
            else
            {
                m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, 0);
            }
            if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
            {
                m_rigidBody2D.velocity = new Vector2(0, 0);
                animator.SetBool("isRunning", false);
                emission.enabled = false;
            }
            else
            {
                if (!isSteping)
                {
                    isSteping = true;
                    StartCoroutine(steps());
                }
            }
            if (m_rigidBody2D.velocity.magnitude > m_speed)
            {
                emission.enabled = true;
                float velMax = Mathf.Sqrt(Mathf.Pow(m_speed, 2) / 2);
                if (m_rigidBody2D.velocity.x > 0 && m_rigidBody2D.velocity.y > 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
                    m_rigidBody2D.velocity = new Vector2(velMax, velMax);
                }
                if (m_rigidBody2D.velocity.x < 0 && m_rigidBody2D.velocity.y > 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 225f);
                    m_rigidBody2D.velocity = new Vector2(-velMax, velMax);
                }
                if (m_rigidBody2D.velocity.x > 0 && m_rigidBody2D.velocity.y < 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
                    m_rigidBody2D.velocity = new Vector2(velMax, -velMax);
                }
                if (m_rigidBody2D.velocity.x < 0 && m_rigidBody2D.velocity.y < 0)
                {
                    dust.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
                    m_rigidBody2D.velocity = new Vector2(-velMax, -velMax);
                }
            }
        }
        else
        {
            m_rigidBody2D.velocity = new Vector2(0, 0);
            if (Random.Range(0, 2) == 0) { animator.SetBool("Right", true); }
            else { animator.SetBool("Right", false); }
            animator.SetBool("Died", true);
        }
    }

    IEnumerator fireGun()
    {
        bulletFire.SetActive(true);
        isShooting = true;
        yield return new WaitForSeconds(0.07f);
        bulletFire.SetActive(false);
        GameObject newBullet = Instantiate(bullet, m_weapon.position, m_weapon.rotation);
        newBullet.transform.Rotate(m_weapon.rotation.x, m_weapon.rotation.y, m_weapon.rotation.z);
        newBullet.transform.parent = null;
        //yield return new WaitForSeconds(0.02f);
        bulletFire.SetActive(false);
        yield return new WaitForSeconds(0.35f);
        isShooting = false;
    }

    IEnumerator steps()
    {
        if (Random.Range(0, 2) == 1)
        {
            audioSourceSteps.PlayOneShot(step1);
        }
        else
        {
            audioSourceSteps.PlayOneShot(step2);
        }
        int auxRand = Random.Range(0, 4);
        if (auxRand == 0)
        {
            yield return new WaitForSeconds(0.15f);
        }
        else if (auxRand == 1)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else if (auxRand == 2)
        {
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
        }
        isSteping = false;
    }

    IEnumerator hit()
    {
        isHit = true;
        m_speed = 6f;
        SpriteRenderer sr = this.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        Color previous = sr.color;
        sr.color = new Color(1f, 0.29f, 0.29f, 1f);
        yield return new WaitForSeconds(1f);
        sr.color = previous;
        m_speed = 4f;
        isHit = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !isHit && !isDead)
        {
            if (Random.Range(0, 2) == 0) { damageAudioSource.PlayOneShot(damage1); }
            else { damageAudioSource.PlayOneShot(damage2); }
            m_playerSO.setHealth(m_playerSO.getHealth() - 1);
            StartCoroutine(hit());
        }
        if (other.gameObject.tag == "BoxOfAmmo" && m_playerSO.getAmmo() < m_playerSO.m_maxAmmo)
        {
            audioSourceSteps.PlayOneShot(ammoPickUp);
            m_playerSO.setAmmo(m_playerSO.m_Ammo + 5);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "HeartContainer" && m_playerSO.getHealth() < m_playerSO.m_maxHealth)
        {
            audioSourceSteps.PlayOneShot(heartPickUp);
            m_playerSO.setHealth(m_playerSO.m_health + 1);
            Destroy(other.gameObject);
        }
    }
}
