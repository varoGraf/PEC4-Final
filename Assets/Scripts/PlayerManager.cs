using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D m_rigidBody2D;
    private Transform m_weapon;
    private float m_weaponAngle;
    public float m_speed = 4f;


    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        m_rigidBody2D = this.GetComponent<Rigidbody2D>();
        m_weapon = this.transform.GetChild(1);
        m_weaponAngle = 0;
    }

    void FixedUpdate()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y + GetComponent<BoxCollider2D>().bounds.size.y / 2, transform.position.z);
        difference.Normalize();

        float rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
        Vector3 playerPosition = new Vector3(this.transform.position.x, this.transform.position.y + GetComponent<BoxCollider2D>().bounds.size.y / 2, this.transform.position.z);

        m_weapon.RotateAround(playerPosition, new Vector3(0, 0, 1), rotationZ - m_weaponAngle);
        m_weaponAngle = rotationZ;
        Debug.DrawRay(m_weapon.position, m_weapon.right * 20, Color.green);

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
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                animator.SetBool("isRunning", true);
                m_rigidBody2D.velocity = new Vector2(m_speed, m_rigidBody2D.velocity.y);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                animator.SetBool("isRunning", true);
                m_rigidBody2D.velocity = new Vector2(-m_speed, m_rigidBody2D.velocity.y);
            }
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
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
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                animator.SetBool("isRunning", true);
                m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, m_speed);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                animator.SetBool("isRunning", true);
                m_rigidBody2D.velocity = new Vector2(m_rigidBody2D.velocity.x, -m_speed);
            }
            if (Input.GetAxisRaw("Vertical") == 0)
            {
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
        }
        if (m_rigidBody2D.velocity.magnitude > m_speed)
        {
            float velMax = Mathf.Sqrt(Mathf.Pow(m_speed, 2) / 2);
            if (m_rigidBody2D.velocity.x > 0 && m_rigidBody2D.velocity.y > 0)
            {
                m_rigidBody2D.velocity = new Vector2(velMax, velMax);
            }
            if (m_rigidBody2D.velocity.x < 0 && m_rigidBody2D.velocity.y > 0)
            {
                m_rigidBody2D.velocity = new Vector2(-velMax, velMax);
            }
            if (m_rigidBody2D.velocity.x > 0 && m_rigidBody2D.velocity.y < 0)
            {
                m_rigidBody2D.velocity = new Vector2(velMax, -velMax);
            }
            if (m_rigidBody2D.velocity.x < 0 && m_rigidBody2D.velocity.y < 0)
            {
                m_rigidBody2D.velocity = new Vector2(-velMax, -velMax);
            }
        }
    }
}
