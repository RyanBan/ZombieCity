using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    private float turnAngle = 180;
    private float moveSpeed = 10;
    public AudioSource sounds;
    public float health = 50f;
    public ParticleSystem smoke;
    public ParticleSystem trailBlack;
    public ParticleSystem fireBall;
    public ParticleSystem dust;
    public ParticleSystem flare;
    public ParticleSystem shockWave;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void Update()
    {
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
        Destroy(gameObject.GetComponent<Collider>());
        moveSpeed = 0;
        sounds.Pause();
        smoke.Play();
        trailBlack.Play();
        fireBall.Play();
        dust.Play();
        flare.Play();
        shockWave.Play();
    }
    public void Stop_()
    {
        moveSpeed = 0;
    }

    // Cars will move back and fofrth between two points
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Wall"))
        {
            //Debug.Log(gameObject.name + "was triggered by" + other.gameObject.name);
            transform.Rotate(0, turnAngle, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "MainChar")
        {

        }
        if (collision.gameObject.name.Contains("Wall"))
        {
            transform.Rotate(0, turnAngle, 0);
            //Debug.Log(gameObject.name + "has collided with" + collision.gameObject.name);
        }
    }
}
