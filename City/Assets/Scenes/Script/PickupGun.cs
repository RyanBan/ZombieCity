using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public Transform targetBone;

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem flash;
    public AudioSource sounds;
    public GameObject impactEffect;

    private GameObject equippedGun;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && equippedGun != null)
        {
            gameObject.SetActive(false);
            equippedGun.gameObject.SetActive(true);

            Shoot();
            sounds.Play();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "MainChar")
        {
            if (!AlreadyHalreadyHasChildObject())
            {
                equip();
            }
            else
            {
                remove();
                equip();
            }
        }
        

    }

    private bool AlreadyHalreadyHasChildObject()
    {
        foreach (Transform child in targetBone)
        {
            if (child.name == "RPG7" || child.name == "Uzi")
            {
                return true;
            }
        }
        return false;
    }


    void remove()
    {
        foreach (Transform child in targetBone)
        {
            if (child.name == "RPG7" || child.name == "Uzi")
            {
                Destroy(child.gameObject);
            }
        }
    }

    void equip()
    {
        equippedGun = gameObject;
        Destroy(gameObject.GetComponent<Collider>());
        gameObject.transform.parent = targetBone;
        gameObject.transform.localPosition = positionOffset;
        gameObject.transform.localEulerAngles = rotationOffset;
        
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform);
            ZombieCharacterControl zombie = hit.transform.GetComponent<ZombieCharacterControl>();
            CarController car = hit.transform.GetComponent<CarController>();
            TruckController truck = hit.transform.GetComponent<TruckController>();


            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }

            if(car != null)
            {
                car.TakeDamage(damage);
            }

            if(truck != null)
            {
                truck.TakeDamage(damage);
            }
        }
        Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
