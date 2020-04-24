using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    float Speed = 2;
    GameObject Pizza;
    float speed = 1;


    private void Start()
    {
        Pizza = GameObject.FindGameObjectWithTag("Pizza");
    }
    // Update is called once per frame
    void Update()
    {

        if (FlySpawner.AllowFly == true)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Pizza.transform.position, step);
            //transform.right = (4 * Pizza.transform.position - transform.position);
            //Lookat();
            //transform.LookAt((2 * transform.position - Pizza.transform.position));
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }

    /*void Lookat()
    {
        Vector3 targetDirection = Pizza.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }*/
}
