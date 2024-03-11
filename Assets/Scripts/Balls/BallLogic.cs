using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BallLogic : MonoBehaviour
{
    public SplineContainer _spline;
    public string _color;

    public float _speed = 5;
    public float _distancePercentage;
    public float _leveldistance;

    private float _splineLength;
    private bool _isTriggered = false;
    public bool _inline;

    private void Start()
    {
        if(_spline == null) GetComponent<Rigidbody2D>().AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition)* 10f);
    }

    private void Update()
    {
        if (_spline != null)
        {
            _splineLength = _spline.CalculateLength();
            _distancePercentage += _speed * Time.deltaTime / _splineLength;
            Vector3 currentPos = _spline.EvaluatePosition(_distancePercentage);
            transform.position = Vector3.MoveTowards(transform.position, currentPos, _speed);
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
            _distancePercentage = other.gameObject.GetComponent<BallLogic>()._distancePercentage;
            _speed = other.gameObject.GetComponent<BallLogic>()._speed;
            transform.position = other.gameObject.transform.position ;
            gameObject.transform.SetParent(GameObject.Find("Pool").transform);
            transform.SetSiblingIndex(other.gameObject.transform.GetSiblingIndex()+1);
            
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("STAY");
        if (other.gameObject.CompareTag("Ball") && _inline==false)
        {
            _distancePercentage += 0.01f;
            for (int i = transform.GetSiblingIndex()+1; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).GetComponent<BallLogic>()._distancePercentage += 0.01f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball")) _inline = true;
    }
}
