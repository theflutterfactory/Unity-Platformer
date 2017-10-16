using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
  void Start()
  {

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
    if (scene.name == Tags.GAMEPLAY_SCENE)
    {
      if (GameManager.instance && GameManager.instance.gameStartedFromMainMenu)
      {
        print("gamestartedfrom main");
      }
    }
  }
}
