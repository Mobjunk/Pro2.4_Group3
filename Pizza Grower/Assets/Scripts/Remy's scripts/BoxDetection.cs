using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    //public BoxCollider2D pizzaBox;
    public BoxCollider2D[] pizzaBox;
    public CircleCollider2D pizza;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void OnCollisionEnter2D(Collision2D collision )
    //  {
    //if (collision.gameObject.name == "Pizza")
    //{
    //     Debug.Log("touched the box");
    // }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (other.gameObject.name == "Pizza")
        //{
            Debug.Log("touched the box");
        //}
    }

}
