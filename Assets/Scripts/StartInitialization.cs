using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInitialization : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", 200);
            PlayerPrefs.SetInt("Level", 1);
        }
    }
}
