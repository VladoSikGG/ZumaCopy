using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "BallInfo", menuName = "Gameplay/New ball info")]
public class BallInfo : ScriptableObject
{
    public GameObject pref;
    public Sprite sprite;
    public string color;
    
}
