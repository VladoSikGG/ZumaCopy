using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanon : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = -1 * dir;
    }
}
