using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;

    [SerializeField] private float _speed;
    [SerializeField] private float _distancePercentage;

    private float _splineLength;

    private void Start()
    {
        _splineLength = _spline.CalculateLength();
    }

    private void Update()
    {
        _distancePercentage += _speed * Time.deltaTime / _splineLength;
        Vector3 currentPos = _spline.EvaluatePosition(_distancePercentage);
        transform.position = new Vector2(currentPos.x, currentPos.y);
    }
}
