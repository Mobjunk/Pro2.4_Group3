using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float Speed = 5;
    GameObject Pizza;


    private void Start()
    {
        Pizza = GameObject.FindGameObjectWithTag("Pizza");
    }
    // Update is called once per frame
    void Update()
    {
        if (FlyDetection.AllowFly == true)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Pizza.transform.position, step);
            transform.LookAt((transform.position - Pizza.transform.position));
        }
    }
}
