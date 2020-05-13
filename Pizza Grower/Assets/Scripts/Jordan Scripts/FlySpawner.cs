using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    public GameObject Fly;
    private Vector2 linePos1;
    private Vector2 linePos2;
    private Vector2 linePos3;
    private Vector2 linePos4;
    private float x;
    private float y;
    private float x1;
    private float y1;
    private int number;
    private float nextSpawn = 1.5f;
    private float Spawned = 0f;
    public static bool AllowFly = true;

    private void Start()
    {
        linePos1 = GameObject.FindGameObjectWithTag("LinePos1").transform.position;
        linePos2 = GameObject.FindGameObjectWithTag("LinePos2").transform.position;
        linePos3 = GameObject.FindGameObjectWithTag("LinePos3").transform.position;
        linePos4 = GameObject.FindGameObjectWithTag("LinePos4").transform.position;
    }

    private void Update()
    {
        if (AllowFly == true)
        {
            if (Time.time > Spawned)
            {
                FlySpawned();
                Spawned = Time.time + nextSpawn;
            }
        }
    }

    void FlySpawned()
    {
        number = Random.Range(0, 10);
        if (number >= 5)
        {      
            x1 = Random.Range(linePos3.x, linePos4.x);
            y1 = Random.Range(linePos3.y, linePos4.y);
            var newfly = Instantiate(Fly, new Vector2(x1, y1), Fly.transform.rotation);
            newfly.transform.parent = gameObject.transform;
            //this.transform.position = new Vector2(x, y);
        }
        else if (number < 5)
        {
            x = Random.Range(linePos1.x, linePos2.x);
            y = Random.Range(linePos1.y, linePos2.y);
            var newfly = Instantiate(Fly, new Vector2(x, y), Fly.transform.rotation);
            newfly.transform.parent = gameObject.transform;
            //this.transform.position = new Vector2(x, y);
        }
    }
}
