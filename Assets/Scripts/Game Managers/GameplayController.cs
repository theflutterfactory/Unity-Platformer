using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

  private Text scoreText, healthText, levelText;

  private float score, health, level;

  void Awake()
  {
    scoreText = GameObject.Find(Tags.SCORE_TEXT).GetComponent<Text>();
    healthText = GameObject.Find(Tags.HEALTH_TEXT).GetComponent<Text>();
    levelText = GameObject.Find(Tags.LEVEL_TEXT).GetComponent<Text>();
  }

  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneWasLoaded;
  }

  void OnDisable()
  {
    SceneManager.sceneLoaded -= OnSceneWasLoaded;
  }

  void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
  {
    if (scene.name == Tags.GAMEPLAY_SCENE && GameManager.instance)
    {
      if (GameManager.instance.gameStartedFromMainMenu)
      {
        GameManager.instance.gameStartedFromMainMenu = false;
        score = 0;
        health = 3;
        level = 0;
      }
      else if (GameManager.instance.gameRestartedPlayerDied)
      {
        GameManager.instance.gameRestartedPlayerDied = false;
        score = GameManager.instance.score;
      }

      scoreText.text = score.ToString();
      healthText.text = health.ToString();
      levelText.text = level.ToString();
    }
  }
}
