using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BallLogic : MonoBehaviour
{
    [SerializeField] private string _color;
    
    [SerializeField] private SplineContainer _spline;
    
    private float _speed = 1;
    private float _distancePercentage;
    private float _splineLength;
    
    private bool _isTriggered = false;
    public bool _inline;
    private float levelDistance;

    public bool movingBack;

    private void Start()
    {
        levelDistance = GameObject.Find("Pool").GetComponent<PoolCheck>().levelDistance;
        if(_spline == null) GetComponent<Rigidbody2D>().AddForce(Camera.main.ScreenToWorldPoint(Input.mousePosition)* 10f);
    }

    private void Update()
    {
        if (_spline != null && !movingBack)
        {
            _splineLength = _spline.CalculateLength();
            _distancePercentage += _speed * Time.deltaTime / _splineLength;
            Vector3 currentPos = _spline.EvaluatePosition(_distancePercentage);
            transform.position = Vector3.MoveTowards(transform.position, currentPos, _speed);
        }
        else if (_spline != null && movingBack)
        {
            _splineLength = _spline.CalculateLength();
            _distancePercentage -= _speed * Time.deltaTime / _splineLength;
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
            BallLogic ballLogic = other.gameObject.GetComponent<BallLogic>();

            _spline = ballLogic.GetSpline();
            _distancePercentage = ballLogic.GetPercentSpline();//+ levelDistance;
            _speed = ballLogic.GetSpeed();
            transform.position = other.gameObject.transform.position ;
            gameObject.transform.SetParent(GameObject.Find("Pool").transform);
            
            // int objIndex = (other.gameObject.transform.GetSiblingIndex() > 0)?
            //     other.gameObject.transform.GetSiblingIndex() - 1 : 0;
            transform.SetSiblingIndex(other.gameObject.transform.GetSiblingIndex());
            InsertBall();
        }

        else if (other.gameObject.CompareTag("Ball")
                 && other.gameObject.GetComponent<BallLogic>().movingBack)
        {
            other.gameObject.GetComponent<BallLogic>().MoveForward();
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        // Debug.Log("STAY");
        // if (other.gameObject.CompareTag("Ball") && _inline==false)
        // {
        //     InsertBall();
        // }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _inline = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "StartWay" && transform.GetSiblingIndex() != transform.parent.childCount-1)
        {
            transform.parent.GetChild(transform.GetSiblingIndex()+1).gameObject.SetActive(true);
        }
    }

    public SplineContainer GetSpline()
    {
        return _spline;
    }

    public float GetPercentSpline()
    {
        return _distancePercentage;
    }

    public void MoveBack()
    {
        movingBack = true;
    }

    public void MoveForward()
    {
        movingBack = false;
    }
    public float GetSpeed()
    {
        return _speed;
    }

    public string GetColor()
    {
        return _color;
    }

    public void AddDistance(float distance)
    {
        _distancePercentage += distance;
    }

    public void InsertBall()
    {
        _distancePercentage += levelDistance;
        for (int i = 0; i < transform.GetSiblingIndex(); i++)
        {
            transform.parent.GetChild(i).GetComponent<BallLogic>().AddDistance(levelDistance);
            Debug.Log(transform.parent.GetChild(i).name);
        }
    }
}
