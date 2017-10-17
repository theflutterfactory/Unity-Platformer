using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
  public GameObject monsterDiedEffect;
  public Transform bullet;
  public float distanceFromPlayerToStartMove = 20f;
  public float speedMin = 1f;
  public float speedMax = 2f;

  private bool moveRight;
  private float movementSpeed;
  private bool isPlayerInRegion;

  private Transform playerTransform;

  public bool canShoot;

  void Start()
  {
    if (Random.Range(0.0f, 1.0f) > 0.5f)
    {
      moveRight = true;
    }
    else
    {
      moveRight = false;
    }

    movementSpeed = Random.Range(speedMin, speedMax);

    playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
  }

  void Update()
  {
    if (playerTransform)
    {
      float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;

      if (distanceFromPlayer < distanceFromPlayerToStartMove)
      {
        if (moveRight)
        {
          Vector3 temp = transform.position;
          temp.x += Time.deltaTime * movementSpeed;
          transform.position = temp;
        }
        else
        {
          Vector3 temp = transform.position;
          temp.x -= Time.deltaTime * movementSpeed;
          transform.position = temp;
        }

        if (!isPlayerInRegion)
        {
          if (canShoot)
          {
            InvokeRepeating("StartShooting", 0.5f, 1f);
          }

          isPlayerInRegion = true;
        }
      }
      else
      {
        CancelInvoke("StartShooting");
      }
    }
  }

  void OnTriggerEnter(Collider target)
  {
    if (target.tag == Tags.PLAYER_BULLET_TAG)
    {
      GameplayController.instance.IncrementScore(200);
      MonsterDied();
    }
  }

  void OnCollisionEnter(Collision target)
  {
    if (target.collider.tag == Tags.PLAYER_TAG)
    {
      MonsterDied();
    }
  }

  void MonsterDied()
  {
    Vector3 effectPos = transform.position;
    effectPos.y += 2f;
    Instantiate(monsterDiedEffect, effectPos, Quaternion.identity);
    gameObject.SetActive(false);
  }

  void StartShooting()
  {
    if (playerTransform)
    {
      Vector3 bulletPos = transform.position;
      bulletPos.y += 1.5f;
      bulletPos.x -= 1f;

      Transform newBullet = (Transform)Instantiate(bullet, bulletPos, Quaternion.identity);
      newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500f);
      newBullet.parent = transform;
    }
  }
}

