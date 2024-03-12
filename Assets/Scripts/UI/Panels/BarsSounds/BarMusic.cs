using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _music;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Music")) GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
    }

    private void Update()
    {
        _music.volume = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("Music", GetComponent<Slider>().value);
    }
}
