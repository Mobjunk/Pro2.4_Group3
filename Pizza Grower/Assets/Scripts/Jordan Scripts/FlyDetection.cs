using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            FlySpawner.AllowFly = false;
            Debug.Log("touched");
            // Game Over Screen Code Here;
        }
    }

    

}
