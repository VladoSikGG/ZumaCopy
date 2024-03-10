using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class BallManager : MonoBehaviour
{
    [SerializeField] private BallInfo _info;
    [SerializeField] private int _nearBalls = 0;
    private GameObject _nearBall;
    public string color;
    public float speed;
    public bool inLine;
    public SplineContainer spline;
    public float percent;
    private void Start()
    {
        //default settings
        if (speed == 0) speed = 2f;
        gameObject.GetComponent<SpriteRenderer>().sprite = _info.sprite;
        color = _info.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ball")
        {
            inLine = true;
            spline = other.gameObject.GetComponent<SplineContainer>();
        }
    }
}
