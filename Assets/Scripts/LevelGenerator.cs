using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

  [SerializeField]
  private int levelLength;

  [SerializeField]
  private int startPlatformLength = 5, endPlatformLength = 5;

  [SerializeField]
  private int distBetweenPlatforms;

  [SerializeField]
  private Transform platformPrefab, platformParent;

  [SerializeField]
  private Transform monster, monsterParent;

  [SerializeField]
  private Transform healthItem, healthItemParent;

  [SerializeField]
  private float platformPosMinY = 0f, platformPosMaxY = 10f;

  [SerializeField]
  private int platformLengthMin = 1, platformLengthMax = 4;

  [SerializeField]
  private float chanceMonsterExists = 0.25f, chanceItemExists = 0.1f;

  [SerializeField]
  private float healthItemMinY = 1f, healthItemMaxY = 3f;

  private float platformLastPosX;

  // Use this for initialization
  void Start()
  {

  }
}
