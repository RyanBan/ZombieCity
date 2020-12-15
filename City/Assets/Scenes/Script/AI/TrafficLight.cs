using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeTag());
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeTag()
    {
        var wait = new WaitForSeconds(20);
        while (true)
        {
            
            gameObject.tag = "GreenLight";
            gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
            gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.gray;
            yield return wait;

            
            gameObject.tag = "RedLight";
            gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.gray;
            gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.red; 
            yield return wait;

        }
    }
}
