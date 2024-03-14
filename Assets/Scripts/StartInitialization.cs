using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInitialization : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("CurrentSkin", 0);
            PlayerPrefs.SetFloat("Sounds", 100);
            PlayerPrefs.SetFloat("Music", 100);
            PlayerPrefs.SetInt("Money", 200);
            PlayerPrefs.SetInt("Level", 1);
        }
    }
}
