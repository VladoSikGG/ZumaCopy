using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    public SplineContainer _spline;

    public float _speed;
    private float _distancePercentage;

    private float _splineLength;
    private bool _isTriggered;

    private void Start()
    {

        _splineLength = _spline.CalculateLength();
    }

    private void Update()
    {
        if (GetComponent<BallManager>().inLine)
        {
            _spline = GetComponent<BallManager>().spline;
            _speed = gameObject.GetComponent<BallManager>().speed;
            _distancePercentage += _speed * Time.deltaTime / _splineLength;
            Vector3 currentPos = _spline.EvaluatePosition(_distancePercentage);
            Vector3 testPos = currentPos - transform.position;
            GetComponent<Rigidbody2D>().MovePosition(currentPos); //= new Vector2(testPos.x, testPos.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition)* 10f);
        }
    }
}
