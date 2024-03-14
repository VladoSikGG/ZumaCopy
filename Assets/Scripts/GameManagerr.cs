using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel, _winPanel, _pausePanel;
    [SerializeField] private AudioSource _winSound, _gameOverSound;
    [SerializeField] private int _numLevel;
    [SerializeField] private List<PoolCheck> _pools;

    [SerializeField] private int _countClearPools;
    [SerializeField] private int _startCountPools;

    private void Start()
    {
        _startCountPools = _pools.Count;
        Time.timeScale = 1;
    }

    private void Update()
    {
        
        for (int i = 0; i < _pools.Count; i++)
        {
            if (_pools[i] != null)
            {
                if (_pools[i].GetStatusPool())
                {
                    _countClearPools++;
                    if (_countClearPools == _startCountPools && _pools.Count == 1)
                    {
                        GameWin();
                    }
                    Destroy(_pools[i]);
                    _pools.RemoveAt(i);
                }
            }
            
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
        _gameOverSound.Play();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        _winPanel.SetActive(true);
        _winSound.Play();
        if (_numLevel == PlayerPrefs.GetInt("Level"))
        {
            int nextLevel = PlayerPrefs.GetInt("Level") + 1;
            PlayerPrefs.SetInt("Level", nextLevel);
        }
        
    }

    public void GamePause()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameContinue()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene($"Level{PlayerPrefs.GetInt("Level")}");
    }
}
