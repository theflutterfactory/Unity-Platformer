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

  private PlayerAnimation playerAnim;

  private BGScroller bgScroller;

  public GameObject smokePosition;

  private bool gameStarted;

  void Awake()
  {
    myBody = GetComponent<Rigidbody>();
    playerAnim = GetComponent<PlayerAnimation>();
    bgScroller = GameObject.Find(Tags.BACKGROUND).GetComponent<BGScroller>();
  }

  void Start()
  {
    StartCoroutine(StartGame());
  }

  void FixedUpdate()
  {
    if (gameStarted)
    {
      Move();
      Grounded();
      Jump();
    }
  }

  void Move()
  {
    myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
  }

  void Grounded()
  {
    isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;

    if (isGrounded && playerJumped)
    {
      playerJumped = false;
      playerAnim.DidLand();
    }
  }

  void Jump()
  {

    if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
    {
      canDoubleJump = false;
      myBody.AddForce(new Vector3(0, secondJumpPower, 0));
    }
    else if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      playerAnim.DidJump();
      myBody.AddForce(new Vector3(0, jumpPower, 0));
      playerJumped = true;
      canDoubleJump = true;
    }
  }

  IEnumerator StartGame()
  {
    yield return new WaitForSeconds(2);
    gameStarted = true;
    playerAnim.PlayerRun();
    bgScroller.canScroll = true;
    smokePosition.SetActive(true);
  }
}
