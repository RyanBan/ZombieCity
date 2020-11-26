using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    // Start is called before the first frame update
    private float turnAngle = 180;
    public float moveSpeed = 8;
    public float health = 50f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip boom;
    [SerializeField] private AudioClip stop;

    public ParticleSystem smoke;
    public ParticleSystem trailBlack;
    public ParticleSystem fireBall;
    public ParticleSystem dust;
    public ParticleSystem flare;
    public ParticleSystem shockWave;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            smoke.Play();
            trailBlack.Play();
            fireBall.Play();
            dust.Play();
            flare.Play();
            shockWave.Play();
            Die();
        }
    }

    void Die()
    {
        moveSpeed = 0;
        audioSource.PlayOneShot(boom);
        audioSource.PlayOneShot(stop);
        Destroy(gameObject.GetComponent<Collider>());
    }

    public void Stop_()
    {
        moveSpeed = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;

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
