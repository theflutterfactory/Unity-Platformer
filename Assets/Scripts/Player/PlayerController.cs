using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

  private PlayerHealthDamageShoot playerShoot;

  private Button jumpBtn;

  void Awake()
  {
    myBody = GetComponent<Rigidbody>();
    playerAnim = GetComponent<PlayerAnimation>();
    bgScroller = GameObject.Find(Tags.BACKGROUND).GetComponent<BGScroller>();
    playerShoot = GetComponent<PlayerHealthDamageShoot>();
    jumpBtn = GameObject.Find(Tags.JUMP_BUTTON).GetComponent<Button>();
    jumpBtn.onClick.AddListener(() => Jump());
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
      if (Input.GetKeyDown(KeyCode.Space))
      {
        Jump();
      }
    }
  }

  void Move()
  {
    myBody.velocity = new Vector3(movementSpeed, myBody.velocity.y, 0f);
  }

  void Grounded()
  {
    isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;
  }

  public void Jump()
  {
    if (!isGrounded && canDoubleJump)
    {
      canDoubleJump = false;
      myBody.AddForce(new Vector3(0, secondJumpPower, 0));
    }

    if (isGrounded)
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
    playerShoot.canShoot = true;
    GameplayController.instance.canCountScore = true;
    smokePosition.SetActive(true);
  }

  void OnCollisionEnter(Collision target)
  {
    if (target.gameObject.tag == Tags.PLATFORM_TAG)
    {
      if (playerJumped)
      {
        playerJumped = false;
        playerAnim.DidLand();
      }
    }
  }
}
