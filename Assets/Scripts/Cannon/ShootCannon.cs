using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootCannon : MonoBehaviour
{
    public GameObject[] _ball;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            GameObject bullet = Instantiate(_ball[Random.Range(0,3)], transform.position, quaternion.identity).GameObject();
            
        }

    }

    

}
