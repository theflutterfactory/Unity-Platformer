using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDamageShoot : MonoBehaviour
{
  private float distanceBetweenNewPlatforms = 120f;

  private LevelGenerator levelGenerator;

  void Awake()
  {
    levelGenerator = GameObject.Find(Tags.LEVEL_GENERATOR).GetComponent<LevelGenerator>();
  }

  void OnTriggerEnter(Collider target)
  {
    if (target.tag == Tags.MORE_PLATFORMS)
    {
      Vector3 temp = target.transform.position;
      temp.x += distanceBetweenNewPlatforms;
      target.transform.position = temp;

      levelGenerator.GenerateLevel(false);
    }
  }
}
