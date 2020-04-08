using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Resize : MonoBehaviour
{
    Vector3 size;
    //Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        size = this.transform.localScale;
        //rotation = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("it works");
            size.y += 0.05f;
            size.x += 0.05f;
            //rotation.z += 0.05f;
            this.transform.localScale = size;
        }
    }
}
