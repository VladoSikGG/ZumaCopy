using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    public GameObject _ball;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

            GameObject bullet = Instantiate(_ball, transform.position, quaternion.identity).GameObject();
            
        }

    }

    

}
