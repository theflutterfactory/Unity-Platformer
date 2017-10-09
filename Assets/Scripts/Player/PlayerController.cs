using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float movementSpeed = 5f;
  public float jumpPower = 10f;
  public float secondJumpPower = 10f;
  public Transform groundCheckPosition;
  public float radius = 0.3f;
  public LayerMask layerGround;

  private Rigidbody myBody;
  private bool isGrounded;
  private bool playerJumped;
  private bool canDoubleJump;

  void Awake()
  {
    myBody = GetComponent<Rigidbody>();
  }

  void Update()
  {

  }

  void FixedUpdate()
  {
    Move();
    Grounded();
    Jump();
  }

  void Move()
  {
    myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
  }

  void Grounded()
  {
    isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;
  }

  void Jump()
  {
    if (Input.GetKey(KeyCode.Space) && isGrounded)
    {
      myBody.AddForce(new Vector3(0, jumpPower, 0));
    }
  }
}
