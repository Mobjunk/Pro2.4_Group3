using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionResizer : MonoBehaviour
{
    float baseAspect = 1280.0f / 800.0f;

    float currAspect = 1.0f * Screen.width / Screen.height;

    public void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Scale(new Vector3(currAspect / baseAspect, 1.0f, 1.0f)) * Camera.main.projectionMatrix;
    }
}
