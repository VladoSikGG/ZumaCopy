using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoViewLevel : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TMP_Text>().text = $"LEVEL: {PlayerPrefs.GetInt("Level")}";
    }
}
