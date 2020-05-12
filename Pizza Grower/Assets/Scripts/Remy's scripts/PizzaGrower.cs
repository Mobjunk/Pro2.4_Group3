using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PizzaGrower : MonoBehaviour
{
    public Vector3 size;

    Vector3 rotation = new Vector3(0f, 0f, 100f);

    public bool Click;

    public BoxDetection BoxDetection;

    // Start is called before the first frame update
    void Start()
    {
        //size = this.transform.localScale
        size = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            BoxDetection.holeCheck();
            Click = true;

            Debug.Log("it works");
            size.y += 0.05f;
            size.x += 0.05f;
            this.transform.Rotate(rotation * Time.deltaTime);
            this.transform.localScale = size;
        }

        if  (Input.GetMouseButtonUp(0))
        {
            Click = false;
            Debug.Log("pizza is done");

        }

      
    }
}
