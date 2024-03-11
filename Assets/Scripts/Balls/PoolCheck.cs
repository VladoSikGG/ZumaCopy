using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolCheck : MonoBehaviour
{
    private int _lastCount, _nearSomeBalls;
    

    private void Update()
    {
        if (_lastCount != gameObject.transform.childCount)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).GetComponent<BallLogic>()._color == 
                    gameObject.transform.GetChild(i+1).GetComponent<BallLogic>()._color)
                {
                    _nearSomeBalls++;
                    if (_nearSomeBalls ==2)
                    {
                        Destroy(gameObject.transform.GetChild(i));
                        Destroy(gameObject.transform.GetChild(i+1));
                        Destroy(gameObject.transform.GetChild(i-1));
                    }
                }
            }
        }
    }
}
