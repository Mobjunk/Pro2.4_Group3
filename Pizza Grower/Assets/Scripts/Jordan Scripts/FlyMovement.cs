using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float Speed = 2;
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
            Lookat();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }

    void Lookat()
    {
        var dir = Pizza.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
