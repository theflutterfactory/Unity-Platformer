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

  private enum PlatformType { None, Flat }

  private class PlatformPositionInfo
  {
    public PlatformType platformType;
    public float postionY;
    public bool hasMonster;
    public bool hasHealthItem;

    public PlatformPositionInfo(PlatformType type, float postionY, bool hasMonster, bool hasHealthItem)
    {
      this.platformType = type;
      this.postionY = postionY;
      this.hasMonster = hasMonster;
      this.hasHealthItem = hasHealthItem;
    }
  }

  void Start()
  {
    GenerateLevel(true);
  }

  void FillPositionInfo(PlatformPositionInfo[] platformInfo)
  {
    int currentPlatformInfoIndex = 0;

    //First Platform section
    for (int i = 0; i < startPlatformLength; i++)
    {
      platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
      platformInfo[currentPlatformInfoIndex].postionY = 0f;
      currentPlatformInfoIndex++;
    }

    //All other platform sections before end Platform section
    while (currentPlatformInfoIndex < levelLength - endPlatformLength)
    {

      //Creates gaps between platform groups. If the previous index is Flat, ie at end of
      //start group, we skip one and then create a new platform.
      //Note: Individual platforms in a group can have various Y positions
      if (platformInfo[currentPlatformInfoIndex - 1].platformType != PlatformType.None)
      {
        currentPlatformInfoIndex++;
        continue;
      }

      float platformPositionY = Random.Range(platformPosMinY, platformPosMaxY);

      int platformLength = Random.Range(platformLengthMin, platformLengthMax);

      for (int i = 0; i < platformLength; i++)
      {
        bool monsterExists = (Random.Range(0f, 1f) < chanceMonsterExists);
        bool healthItemExists = (Random.Range(0f, 1f) < chanceItemExists);

        platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
        platformInfo[currentPlatformInfoIndex].postionY = platformPositionY;
        platformInfo[currentPlatformInfoIndex].hasMonster = monsterExists;
        platformInfo[currentPlatformInfoIndex].hasHealthItem = healthItemExists;

        currentPlatformInfoIndex++;

        //If we touch the beginning of the End platforms
        if (currentPlatformInfoIndex > (levelLength - endPlatformLength))
        {
          currentPlatformInfoIndex = levelLength - endPlatformLength;
          break;
        }
      }

      //End platforms
      for (int i = 0; i < endPlatformLength; i++)
      {
        platformInfo[currentPlatformInfoIndex].platformType = PlatformType.Flat;
        platformInfo[currentPlatformInfoIndex].postionY = 0f;

        currentPlatformInfoIndex++;
      }
    }
  }

  void CreatePlatformsFromPositionInfo(PlatformPositionInfo[] platformPositionInfo, bool gameStarted)
  {
    for (int i = 0; i < platformPositionInfo.Length; i++)
    {
      PlatformPositionInfo positionInfo = platformPositionInfo[i];

      if (positionInfo.platformType == PlatformType.None)
      {
        continue;
      }

      Vector3 platformPosition;

      if (gameStarted)
      {
        platformPosition = new Vector3(distBetweenPlatforms * i, positionInfo.postionY, 0);
      }
      else
      {
        platformPosition = new Vector3(distBetweenPlatforms + platformLastPosX, positionInfo.postionY, 0);
      }

      platformLastPosX = platformPosition.x;

      //Group platforms under one GameObject in the Unity editor for clarity
      Transform createBlock = (Transform)Instantiate(platformPrefab, platformPosition, Quaternion.identity);
      createBlock.parent = platformParent;

      if (positionInfo.hasMonster)
      {
        if (gameStarted)
        {
          platformPosition = new Vector3(distBetweenPlatforms * i, positionInfo.postionY + 0.1f, 0);
        }
        else
        {
          platformPosition = new Vector3(distBetweenPlatforms + platformLastPosX, positionInfo.postionY + 0.1f, 0);
        }

        //Group monsters
        Transform createMonster = (Transform)Instantiate(monster, platformPosition, Quaternion.Euler(0, -90, 0));
        createMonster.parent = monsterParent;
      }

      if (positionInfo.hasHealthItem)
      {
        if (gameStarted)
        {
          platformPosition = new Vector3(distBetweenPlatforms * i, positionInfo.postionY +
          Random.Range(healthItemMinY, healthItemMaxY), 0);
        }
        else
        {
          platformPosition = new Vector3(distBetweenPlatforms + platformLastPosX,
          positionInfo.postionY + Random.Range(healthItemMinY, healthItemMaxY), 0);
        }
        //Group health items
        Transform createHealthItem = Instantiate(healthItem, platformPosition, Quaternion.identity);
        createHealthItem.parent = healthItemParent;
      }
    }
  }

  public void GenerateLevel(bool gameStarted)
  {
    PlatformPositionInfo[] platformInfo = new PlatformPositionInfo[levelLength];

    for (int i = 0; i < platformInfo.Length; i++)
    {
      platformInfo[i] = new PlatformPositionInfo(PlatformType.None, -1, false, false);
    }

    FillPositionInfo(platformInfo);
    CreatePlatformsFromPositionInfo(platformInfo, gameStarted);
  }
}
