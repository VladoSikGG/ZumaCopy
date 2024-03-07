using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private BallInfo _info;
    [SerializeField] private int _nearBalls = 0;
    private GameObject _nearBall;
    public string color;
    public float speed;
    private void Start()
    {
        //default settings
        if (speed == 0) speed = 2f;
        gameObject.GetComponent<SpriteRenderer>().sprite = _info.sprite;
        color = _info.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (speed < 0) speed = speed * -1;

        if (other.gameObject.GetComponent<BallManager>().color == color)
        {
            _nearBall = other.gameObject;
            _nearBalls++;
            if (_nearBalls >= 2)
            {
                Destroy(other.gameObject);
                Destroy(_nearBall);
                Destroy(gameObject);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (speed > 0) speed = speed * -1;
        if (other.gameObject.GetComponent<BallManager>().color == color)
            _nearBalls--;
    }
}
