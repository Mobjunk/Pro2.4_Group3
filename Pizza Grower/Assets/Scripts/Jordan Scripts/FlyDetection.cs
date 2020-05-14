using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDetection : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            FlySpawner.AllowFly = false;
            GameHandler gameHandler = other.gameObject.GetComponentInParent<Regular>();
            gameHandler.HandlePizzaCompletion(false);
        }
    }

    

}
