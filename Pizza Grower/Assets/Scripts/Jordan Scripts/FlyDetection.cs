using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDetection : MonoBehaviour
{
    public static bool AllowFly = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fly"))
        {
            AllowFly = false;
            // Game Over Screen Code Here;
        }
    }
}
