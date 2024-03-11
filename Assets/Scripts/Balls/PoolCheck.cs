using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolCheck : MonoBehaviour
{
    public int _lastCount, _nearSomeBalls;


    private void Start()
    {
        _lastCount = gameObject.transform.childCount;
    }

    private void Update()
    {
        if (_lastCount != gameObject.transform.childCount)
        {
            _lastCount = gameObject.transform.childCount;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Debug.Log($"iter{i}: {gameObject.transform.GetChild(i).name}");
                if (gameObject.transform.GetChild(i).GetComponent<BallLogic>()._color == 
                    gameObject.transform.GetChild(i+1).GetComponent<BallLogic>()._color)
                {
                    _nearSomeBalls++;
                    if (_nearSomeBalls == 2)
                    {
                        Destroy(gameObject.transform.GetChild(i-1).gameObject);
                        Destroy(gameObject.transform.GetChild(i).gameObject);
                        Destroy(gameObject.transform.GetChild(i+1).gameObject);
                        _nearSomeBalls = 0;
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
