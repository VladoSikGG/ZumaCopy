using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressedButton : MonoBehaviour
{
    [SerializeField] private GameObject _selfPanel;
    [SerializeField] private Sprite _notActiveView;

    private void Awake()
    {
        _notActiveView = GetComponent<Image>().sprite;
    }

    private void Update()
    {
        if (!_selfPanel.activeInHierarchy) GetComponent<Image>().sprite = _notActiveView;
    }
}
