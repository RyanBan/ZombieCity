using UnityEngine;
using System.Collections.Generic;

public class ZombieCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Walk,
        Wander,
        Run
    }

    [SerializeField] public float m_moveSpeed = 2;
    [SerializeField] public float m_turnSpeed = 200;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Walk;
    [SerializeField] private AudioClip die;
    private AudioSource audioSource;

    private float m_currentV = 1;
    private float m_currentH = 0;

    private float turnAngle = 180;

    private readonly float m_interpolation = 10;



    public float health = 50f;

    void Awake()
    {
        if(!m_animator) { gameObject.GetComponent<Animator>(); }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //attack when collide with the character 
        if(collision.gameObject.name == "MainChar")
        {
            m_animator.SetTrigger("Attack");   
            //Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);
        }
        //go back and forth
        if (collision.gameObject.name.Contains("Wall"))
        {
            transform.Rotate(0, turnAngle, 0);
            //Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);
        }
        //dead when collide with a car
        if (collision.gameObject.name.Contains("Car"))
        {
            //Debug.Log(gameObject.name + " was triggered by " + collision.gameObject.name);
            Die();
            m_rigidBody.AddForce(new Vector3(0, 500, 500));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Wall"))
        {
            //Debug.Log(gameObject.name + "was triggered by" + other.gameObject.name);
            transform.Rotate(0, turnAngle, 0);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(die);
        m_animator.SetTrigger("Dead");
        m_moveSpeed = 0;
    }

    void FixedUpdate ()
    {
        switch(m_controlMode)
        {
            case ControlMode.Walk:
                DefaultUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }
    private void DefaultUpdate()
    {
        m_animator.SetFloat("MoveSpeed", m_moveSpeed * 100);
    }

}
