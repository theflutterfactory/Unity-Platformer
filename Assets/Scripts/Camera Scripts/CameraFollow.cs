using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  private Transform playerTransform;
  public float offsetZ = -15f;
  public float offsetX = -5f;
  public float constantY = 5f;
  public float cameraLerpTime = 0.05f;

  void Awake()
  {
    playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
  }

  void Update()
  {
    if (playerTransform)
    {
      Vector3 playerPosition = new Vector3(playerTransform.position.x + offsetX,
      constantY, playerTransform.position.z + offsetZ);
    }
  }
}
