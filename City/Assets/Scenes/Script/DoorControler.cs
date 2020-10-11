using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControler : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Door"))
        {
            Debug.Log(gameObject.name + "was triggered by" + other.gameObject.name);
            Animator anim = other.GetComponent<Animator>();
            if(Input.GetKeyDown(KeyCode.E))
            anim.SetTrigger("openclose");
        }

    }
}
