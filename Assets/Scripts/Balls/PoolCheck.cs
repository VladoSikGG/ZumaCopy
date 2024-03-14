using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class PoolCheck : MonoBehaviour
{
    private int _lastCount, _nearSomeBalls;
    [SerializeField] private SplineContainer _way;
    [SerializeField] private float _levelSpeed;
    [SerializeField] private GameManagerr _gameManager;
    [Range(0f,1f)]
    public float levelDistance;

    [SerializeField] private int _countBalls;
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private Transform _spawnPosition;

    private bool _isClear;
    

   // private int _ballsForDestroy = 1;

   private void Awake()
   {
       GenerateBalls();
   }

   private void Start()
    {
        _lastCount = gameObject.transform.childCount;
    }

    public bool GetStatusPool()
    {
        return _isClear;
    }
    //MEchanic from Zuma
     private void Update()
     {
         
         if(transform.childCount <= 0 && Time.timeScale != 0) _isClear = true;
         
         if (_lastCount != gameObject.transform.childCount)
         {
             _lastCount = gameObject.transform.childCount;
             for (int i = 0; i < gameObject.transform.childCount-1; i++)
             {
                 BallLogic currentChildScript = transform.GetChild(i).GetComponent<BallLogic>();
                 if (currentChildScript.GetInfoFromPlayer())
                 {
                     List<GameObject> balls = new List<GameObject>();
                     for (int n = i+1; n < transform.childCount; n++)
                     {
                         if (currentChildScript.GetColor() ==
                             transform.GetChild(n).GetComponent<BallLogic>().GetColor())
                         {
                             balls.Add(transform.GetChild(n).gameObject);
                         }
                         else break;
                     }
                     
                     for (int v = i-1; v >= 0; v--)
                     {
                         if (currentChildScript.GetColor() ==
                             transform.GetChild(v).GetComponent<BallLogic>().GetColor())
                         {
                             balls.Add(transform.GetChild(v).gameObject);
                         }
                         else break;
                     }

                     if (balls.Count >= 3) PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+10);
                     if (balls.Count >= 2)
                     {
                         for (int j = 0; j < balls.Count; j++)
                         {
                             Destroy(balls[j].gameObject);
                         }
                         Destroy(currentChildScript.gameObject);
                         for (int j = 0; j < i; j++)
                         {
                             transform.GetChild(j).GetComponent<BallLogic>().MoveBack();
                         }
                         Destroy(currentChildScript.gameObject);
                         balls.Clear();
                         
                     }
                     else
                     {
                         currentChildScript.InsertBall();
                     }
                     
                     // while (currentChildScript.GetColor() ==
                     //        transform.GetChild(i+1).GetComponent<BallLogic>().GetColor())
                     // {
                     //     Destroy(transform.GetChild(i+1).gameObject);
                     //     deleteSelf = true;
                     // }
                     //
                     // while (currentChildScript.GetColor() ==
                     //        transform.GetChild(i-1).GetComponent<BallLogic>().GetColor())
                     // {
                     //     Destroy(transform.GetChild(i-1).gameObject);
                     //     deleteSelf = true;
                     // }
                 }
                 Debug.Log(transform.childCount-1);
                 transform.GetChild(transform.childCount-1).GetComponent<BallLogic>().MoveForward();
                 // Debug.Log($"iter{i}: {gameObject.transform.GetChild(i).name}");
                 // if (gameObject.transform.GetChild(i).GetComponent<BallLogic>().GetColor() == 
                 //     gameObject.transform.GetChild(i+1).GetComponent<BallLogic>().GetColor())
                 // {
                 //     _nearSomeBalls++;
                 //     if (_nearSomeBalls == _ballsForDestroy)
                 //     {
                 //         Destroy(gameObject.transform.GetChild(i-1).gameObject);
                 //         Destroy(gameObject.transform.GetChild(i).gameObject);
                 //         // Destroy(gameObject.transform.GetChild(i+1).gameObject);
                 //         _nearSomeBalls = 0;
                 //         if (i+1 != transform.childCount-1)
                 //         {
                 //             for (int j = 0; j < i-1; j++)
                 //             {
                 //                 transform.GetChild(j).GetComponent<BallLogic>().MoveBack();
                 //             }
                 //         }
                 //         
                 //     }
                 // }
                 // else
                 // {
                 //     _nearSomeBalls = 0;
                 // }
                 currentChildScript.FromPlayerToLine();
             }
         }
         //_nearSomeBalls = 0;
         
    }
     public float GetSpeed()
     {
         return _levelSpeed;
     }

     private void GenerateBalls()
     {
         for (int i = 0; i < _countBalls; i++)
         {
             GameObject GO = Instantiate(_balls[Random.Range(0, _balls.Length)], _spawnPosition.position, quaternion.identity, transform);
             GO.GetComponent<BallLogic>().SetSpline(_way);
             GO.GetComponent<BallLogic>().SetPool(gameObject);
             if(i != 0) GO.SetActive(false);
         }
     }
}
