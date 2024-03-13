using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel, _winPanel, _pausePanel;
    [SerializeField] private AudioSource _winSound, _gameOverSound;

    private void Start()
    {
        Time.timeScale = 1;
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
        int nextLevel = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", nextLevel);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene($"Level{PlayerPrefs.GetInt("Level")}");
    }
}
