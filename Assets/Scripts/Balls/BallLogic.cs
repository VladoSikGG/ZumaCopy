using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BallLogic : MonoBehaviour
{
    [SerializeField] private string _color;
    
    [SerializeField] private SplineContainer _spline;

    [SerializeField] private float _speedForFly;
    
    private float _speed = 0.5f;
    private float _distancePercentage;
    private float _splineLength;
    
    private bool _fromPlayer = false;
    //public bool _inline;
    private float levelDistance;

    private GameObject _pool;

    public bool movingBack;

    private GameObject _gameManager;

    private void Start()
    {
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        _pool = GameObject.Find("Pool");
        _gameManager = GameObject.Find("GameManager");
        levelDistance = _pool.GetComponent<PoolCheck>().levelDistance;
        _speed = _pool.GetComponent<PoolCheck>().GetSpeed();
        Debug.Log(direction.normalized );
        if (_spline == null) GetComponent<Rigidbody2D>().AddForce(direction.normalized *100*_speedForFly); 
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
        // else
        // {
        //     GetComponent<Rigidbody2D>().AddForce((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)* 10f);
        // }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball") && _spline == null && other.gameObject.GetComponent<BallLogic>().GetSpline() != null)
        {
            _fromPlayer = true;
            // if (GetColor() == other.gameObject.GetComponent<BallLogic>().GetColor())
            // {
            //     if (other.gameObject.transform.GetSiblingIndex() != _pool.transform.childCount-1)
            //     {
            //         for (int j = 0; j < other.gameObject.transform.GetSiblingIndex(); j++) 
            //         {
            //             _pool.transform.GetChild(j).GetComponent<BallLogic>().MoveBack(); 
            //         }
            //     }
            //     Destroy(other.gameObject);
            //     Destroy(gameObject);
            //     
            //     PlayerPrefs.SetInt("DestroyedBalls", PlayerPrefs.GetInt("DestroyedBalls")+2);
            //     if (PlayerPrefs.GetInt("DestroyedBalls") >= 4)
            //     {
            //         PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+10);
            //         PlayerPrefs.SetInt("DestroyedBalls", 0);
            //     }
            // }
            // else
            {
                BallLogic ballLogic = other.gameObject.GetComponent<BallLogic>();

                _spline = ballLogic.GetSpline();
                _distancePercentage = ballLogic.GetPercentSpline();//+ levelDistance;
                _speed = ballLogic.GetSpeed();
                transform.position = other.gameObject.transform.position ;
                gameObject.transform.SetParent(_pool.transform);
                
                transform.SetSiblingIndex(other.gameObject.transform.GetSiblingIndex());
                InsertBall();
            }
            
        }

        //else

        if (other.gameObject.CompareTag("Finish")) _gameManager.GetComponent<GameManagerr>().GameOver();
    }

    public bool GetInfoFromPlayer()
    {
        return _fromPlayer;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball")
            && other.gameObject.GetComponent<BallLogic>().movingBack
            && movingBack != true)
        {
            other.gameObject.GetComponent<BallLogic>().MoveForward();
        }
        // Debug.Log("STAY");
        // if (other.gameObject.CompareTag("Ball") && _inline==false)
        // {
        //     InsertBall();
        // }
        
    }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ball"))
    //     {
    //         _inline = true;
    //     }
    //     
    // }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (_spline != null)
        {
            if (other.gameObject.name == "StartWay" && transform.GetSiblingIndex() != transform.parent.childCount-1)
            {
                transform.parent.GetChild(transform.GetSiblingIndex()+1).gameObject.SetActive(true);
            }
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

    public void SetSpline(SplineContainer spline)
    {
        _spline = spline;
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
