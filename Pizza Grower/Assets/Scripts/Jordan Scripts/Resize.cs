using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Resize : MonoBehaviour
{
    [HideInInspector] public Vector3 size;
    Vector3 rotation = new Vector3(0f, 0f, 100f);
    private Vector3 defaultSize;
    [HideInInspector] public bool isClicking;
    [HideInInspector] public bool blockInput = false;

    public float minGrow = 0.0125f;
    public float maxGrow = 0.075f;
    public float currentGrow;

    // Start is called before the first frame update
    void Start()
    {
        currentGrow = minGrow;
        defaultSize = transform.localScale;
        size = defaultSize;
    }

    // Update is called once per frame
    void Update()
    {
        isClicking = Input.GetMouseButton(0) && !blockInput && TestScript.instance.allowedToGrow;
        if (isClicking && size.x < 1 && size.y < 1)
        {
            size.y += currentGrow;
            size.x += currentGrow;
            transform.Rotate(rotation * Time.deltaTime);
            transform.localScale = size;
        }
    }

    public void Reset()
    {
        size = defaultSize;
        blockInput = false;
    }

    public void BlockInput()
    {
        blockInput = true;
    }

    public void GrowSpeed()
    {
        currentGrow = Random.Range(minGrow, maxGrow);
    }
}
