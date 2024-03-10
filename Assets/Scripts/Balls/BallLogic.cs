using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BallLogic : MonoBehaviour
{
    public SplineContainer _spline;

    public float _speed = 5;
    public float _distancePercentage;

    private float _splineLength;
    private bool _isTriggered = false;
    public bool _inline;
    
    private void Start()
    {
        
    }
    private void Update()
    {
        if (_spline != null)
        {
            _splineLength = _spline.CalculateLength();
            _distancePercentage += _speed * Time.deltaTime / _splineLength;
            Vector3 currentPos = _spline.EvaluatePosition(_distancePercentage);
            transform.position = new Vector2(currentPos.x, currentPos.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition)* 10f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball") && _spline == null)
        {
            _isTriggered = true;
            _spline = other.gameObject.GetComponent<BallLogic>()._spline;
            _distancePercentage = other.gameObject.GetComponent<BallLogic>()._distancePercentage + 0.025f;
            _speed = other.gameObject.GetComponent<BallLogic>()._speed;
            transform.position = other.gameObject.transform.position ;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("STAY");
        if (other.gameObject.CompareTag("Ball"))
        {
            _distancePercentage += 0.025f;
        }
    }
}
