﻿using UnityEngine;
using System.Collections.Generic;

public class ZombieCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Tank;

    private float m_currentV = 1;
    private float m_currentH = 0;

    private float turnAngle = 180;

    private readonly float m_interpolation = 10;

    void Awake()
    {
        if(!m_animator) { gameObject.GetComponent<Animator>(); }
        if(!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "MainChar")
        {
            m_animator.SetTrigger("Attack");   
            Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);
        }
        if (collision.gameObject.name.Contains("Wall"))
        {
            transform.Rotate(0, turnAngle, 0);
            Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);
        }

        if (collision.gameObject.name.Contains("Car"))
        {
            Debug.Log(gameObject.name + " was triggered by " + collision.gameObject.name);
            m_animator.SetTrigger("Dead");
            m_moveSpeed = 0;
            m_rigidBody.AddForce(new Vector3(0, 500, 500));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Wall"))
        {
            Debug.Log(gameObject.name + "was triggered by" + other.gameObject.name);
            transform.Rotate(0, turnAngle, 0);
        }
    }
    void FixedUpdate ()
    {
        switch(m_controlMode)
        {
            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }
    private void TankUpdate()
    {
        transform.position += transform.forward * m_moveSpeed * m_currentV * Time.deltaTime;
        m_animator.SetFloat("MoveSpeed", m_moveSpeed * 100);
    }

}