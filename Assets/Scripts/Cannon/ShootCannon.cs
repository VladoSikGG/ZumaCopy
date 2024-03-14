using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootCannon : MonoBehaviour
{
    [SerializeField] private Sprite[] _playerSkins;
    [SerializeField] private SpriteRenderer _playerRenderer;
    public GameObject[] _ball;
    private GameObject _currentBall,
                       // _nextBall, 
                        _pool;

    [SerializeField] private GameObject _currentBallView; //_nextBallView; 

    public bool _havePool;
    private void Start()
    {
        _pool = FindObjectOfType<PoolCheck>().gameObject;
        _playerRenderer.sprite = _playerSkins[PlayerPrefs.GetInt("CurrentSkin")];
        InitializeCannonBall();
        _currentBall = InitializeCannonBall();
        //_nextBall = InitializeCannonBall();
        ViewBall();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(_currentBall, transform.position, 
                quaternion.identity).GameObject();
            _currentBall = InitializeCannonBall();
            // _currentBall = _nextBall;
            // _nextBall = InitializeCannonBall();
            ViewBall();
        }

    }

    private GameObject InitializeCannonBall()
    {
        _pool = FindObjectOfType<PoolCheck>().gameObject;
        // if (_pool.transform.childCount == 0)
        // {
        //     Destroy(_pool.GetComponent<PoolCheck>());
        //     _pool = FindObjectOfType<PoolCheck>().gameObject;
        // }
        
        GameObject ball = null;
        if (_pool.transform.childCount > 0)
        {
            while (ball == null)
            {
                GameObject tempBall = _ball[Random.Range(0, _ball.Length)];
                for (int i = 0; i < _pool.transform.childCount; i++)
                {
                    if (tempBall.GetComponent<BallLogic>().GetColor() == 
                        _pool.transform.GetChild(i).gameObject.GetComponent<BallLogic>().GetColor())
                    {
                        ball = tempBall;
                    }
                }
            }
        }
        return ball;
    }

    private void ViewBall()
    {
        // _nextBallView.GetComponent<SpriteRenderer>().sprite = 
        //     _nextBall.GetComponent<SpriteRenderer>().sprite;
        if (_currentBall != null)
        {
            _currentBallView.GetComponent<SpriteRenderer>().sprite =
                _currentBall.GetComponent<SpriteRenderer>().sprite;
        }

    }

}
