using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolCheck : MonoBehaviour
{
    private int _lastCount, _nearSomeBalls;
    [Range(0f,1f)]
    public float levelDistance;


    private void Start()
    {
        _lastCount = gameObject.transform.childCount;
    }

    private void Update()
    {
        if (_lastCount != gameObject.transform.childCount)
        {
            _lastCount = gameObject.transform.childCount;
            for (int i = 0; i < gameObject.transform.childCount-1; i++)
            {
                Debug.Log($"iter{i}: {gameObject.transform.GetChild(i).name}");
                if (gameObject.transform.GetChild(i).GetComponent<BallLogic>().GetColor() == 
                    gameObject.transform.GetChild(i+1).GetComponent<BallLogic>().GetColor())
                {
                    _nearSomeBalls++;
                    if (_nearSomeBalls == 2)
                    {
                        Destroy(gameObject.transform.GetChild(i-1).gameObject);
                        Destroy(gameObject.transform.GetChild(i).gameObject);
                        Destroy(gameObject.transform.GetChild(i+1).gameObject);
                        _nearSomeBalls = 0;
                        if (i+1 != transform.childCount-1)
                        {
                            for (int j = 0; j < i-1; j++)
                            {
                                transform.GetChild(j).GetComponent<BallLogic>().MoveBack();
                            }
                        }
                        
                    }
                }
                else
                {
                    _nearSomeBalls = 0;
                }
            }
        }
        _nearSomeBalls = 0;
    }
}
