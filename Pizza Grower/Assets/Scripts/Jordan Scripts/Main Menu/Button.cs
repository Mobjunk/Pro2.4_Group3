﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void Retry()
    {
        //SceneManager.LoadScene("PreviousScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}