using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallInfo", menuName = "Gameplay/New ball info")]
public class BallInfo : ScriptableObject
{
    public Sprite sprite;
    public string color;
}
