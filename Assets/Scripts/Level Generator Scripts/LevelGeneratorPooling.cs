using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorPooling : MonoBehaviour
{
  [SerializeField]
  private Transform platform, platformParent;

  [SerializeField]
  private Transform monster, monsterParent;

  [SerializeField]
  private Transform healthItem, healthItemParent;

  [SerializeField]
  private int levelLength = 100;

  [SerializeField]
  private float distanceBetweenPlatforms = 15f;

  [SerializeField]
  private float positionMinY = 0f, positionMaxY = 7f;

  [SerializeField]
  private float chanceForMonsterExist = 0.25f, chanceHealthItemExist = 0.1f;

  [SerializeField]
  private float healthItemMinY = 1f, healthItemMaxY = 3f;

  private float platformLastPositionX;
  private Transform[] platformArray;

  void Start()
  {
    CreatePlatforms();
  }

  void CreatePlatforms()
  {
    platformArray = new Transform[levelLength];

    for (int i = 0; i < platformArray.Length; i++)
    {
      Transform newPlatform = (Transform)Instantiate(platform, Vector3.zero, Quaternion.identity);
      platformArray[i] = newPlatform;

      float platformPositionY = Random.Range(positionMinY, positionMaxY);

      Vector3 platformPosition;

      if (i < 5)
      {
        platformPositionY = 0f;
      }

      platformPosition = new Vector3(distanceBetweenPlatforms * i, platformPositionY, 0);

      platformLastPositionX = platformPosition.x;

      platformArray[i].position = platformPosition;
      platformArray[i].parent = platformParent;

      SpawnHealthAndMonsters(platformPosition, i, true);
    }
  }

  public void PoolingPlatforms()
  {
    for (int i = 0; i < platformArray.Length; i++)
    {
      if (!platformArray[i].gameObject.activeInHierarchy)
      {
        platformArray[i].gameObject.SetActive(true);
        float platformPositionY = Random.Range(positionMinY, positionMaxY);
        Vector3 platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX,
        platformPositionY, 0);

        platformArray[i].position = platformPosition;

        platformLastPositionX = platformPosition.x;

        SpawnHealthAndMonsters(platformPosition, i, false);
      }
    }
  }

  void SpawnHealthAndMonsters(Vector3 platformPosition, int i, bool gameStarted)
  {
    if (i > 5)
    {
      if (Random.Range(0f, 1f) < chanceForMonsterExist)
      {
        if (gameStarted)
        {
          platformPosition = new Vector3(distanceBetweenPlatforms * i, platformPosition.y + 0.1f, 0);
        }
        else
        {
          platformPosition = new Vector3(distanceBetweenPlatforms * i,
          platformPosition.y + platformLastPositionX, 0);
        }

        Transform createMonster = Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0)) as Transform;
        createMonster.parent = monsterParent;
      }

      if (Random.Range(0f, 1f) < chanceHealthItemExist)
      {
        if (gameStarted)
        {
          platformPosition = new Vector3(distanceBetweenPlatforms * i,
          platformPosition.y + Random.Range(healthItemMinY, healthItemMaxY), 0);
        }
        else
        {
          platformPosition = new Vector3(distanceBetweenPlatforms + platformLastPositionX,
          platformPosition.y + Random.Range(healthItemMinY, healthItemMaxY), 0);
        }

        Transform createHealthItem = Instantiate(healthItem, platformPosition, Quaternion.identity) as Transform;
        createHealthItem.parent = healthItemParent;
      }
    }
  }

}
