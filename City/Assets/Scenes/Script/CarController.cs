using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    private float turnAngle = 180;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;

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
