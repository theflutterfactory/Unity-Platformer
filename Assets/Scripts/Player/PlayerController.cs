using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float movementSpeed = 5f;
  private Rigidbody myBody;

  void Awake()
  {
    myBody = GetComponent<Rigidbody>();
  }

  void Update()
  {

  }

  void FixedUpdate()
  {
    MovePlayer();
  }

  void MovePlayer()
  {
    myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
  }
}
