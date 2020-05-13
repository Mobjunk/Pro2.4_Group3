using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static TestScript instance;

    private void Awake()
    {
        instance = this;
    }

    public bool allowedToGrow = false;

    public void PointerUp()
    {
        allowedToGrow = false;
    }

    public void PointerDown()
    {
        allowedToGrow = true;
    }

}
