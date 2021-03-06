
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {
  [Header("Audio")]
  [SerializeField] AudioSource footstepSFX;
  [Header("Movement Vars")]
  public float walkingSpeed = 7.5f;
  public float pushSpeed = 5f;
  public float pushLookSpeed = 2.0f;
  public float jumpSpeed = 8.0f;
  public float gravity = 20.0f;

  [Header("Look Vars")]
  public Camera playerCamera;
  public float lookSpeed = 2.0f;
  public float lookXLimit = 45.0f;
  public PhysicsScene physicsScene;
  public Vector2 pushLookXMinMax = new Vector2();

  [Header("Reverse Mode")]
  public bool isReverse;

  CharacterController characterController;
  Grabber grabber;
  Vector3 moveDirection = Vector3.zero;
  float rotationX = 0;

  bool camControlsActive = true;
  bool isPushing = false;
  
  private Vector2 moveValue;
  private Vector2 lookValue;

  private Vector3 cachedPosition;
  private Vector3 lastGoodPosition;

  private Quaternion  cachedRotation;
  private Quaternion lastGoodRotation;
  private AudioClip walkingSFXClip;
  private float timer;

  [HideInInspector]
  public bool canMove = true;

  public void SetIsPushing(bool pushing, AudioClip pushingAudio = null) {
    isPushing = pushing;
    if(pushingAudio != null) {
      footstepSFX.clip = pushingAudio;
    }
    else {
      footstepSFX.clip = walkingSFXClip;
    }
  }
  
  public void AllowMovement() {
    float newRotation = playerCamera.transform.rotation.eulerAngles.x;
    if(newRotation > 180) newRotation = newRotation - 360;
    rotationX = newRotation;
    moveValue = new Vector2();
    lookValue = new Vector2();
    canMove = true;
  }

  public void DenyMovement() {
    footstepSFX.Stop();
    canMove = false;
    rotationX = 0;
    moveValue = new Vector2();
    lookValue = new Vector2();
  }

  void Start() {
    characterController = GetComponent<CharacterController>();
    grabber = GetComponent<Grabber>();
    walkingSFXClip = footstepSFX.clip;

    // Lock cursor
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    if(isPushing) {
      Push();
    }
    else {
      Walk();
    }
  }

  void Push() {
    //Do not move pitch the camera, only move and transform character
    Vector3 forward = transform.TransformDirection(Vector3.forward);
    Vector3 right = transform.TransformDirection(Vector3.right);
    float curSpeedX = canMove ? pushSpeed * moveValue.y : 0;
    float curSpeedY = canMove ? pushSpeed * moveValue.x : 0;
    moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    characterController.Move(moveDirection * Time.deltaTime);
    transform.rotation *= Quaternion.Euler(0, lookValue.x * pushLookSpeed, 0);
    rotationX += -lookValue.y * lookSpeed;
    rotationX = Mathf.Clamp(rotationX, pushLookXMinMax.x, pushLookXMinMax.y);
    playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
  }

  void Walk() {
    // We are grounded, so recalculate move direction based on axes
    Vector3 forward = transform.TransformDirection(Vector3.forward);
    Vector3 right = transform.TransformDirection(Vector3.right);
    float curSpeedX = canMove ?  walkingSpeed * moveValue.y : 0;
    float curSpeedY = canMove ? walkingSpeed * moveValue.x : 0;
    float movementDirectionY = moveDirection.y;
    moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    moveDirection.y = movementDirectionY;

    // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
    // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
    // as an acceleration (ms^-2)
    if (!characterController.isGrounded) {
        moveDirection.y -= gravity * Time.deltaTime;
    }

    // Move the controller
    characterController.Move(moveDirection * Time.deltaTime);

    // Player and Camera rotation
    if (canMove) {
        rotationX += -lookValue.y * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        if(camControlsActive) {
          playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
          transform.rotation *= Quaternion.Euler(0, lookValue.x * lookSpeed, 0);
        }
    }
  }
  void OnMove(InputValue value) {
    if(canMove) {
      moveValue = value.Get<Vector2>();
      if(isReverse) moveValue *= -1;
      if(moveValue.x == 0 && moveValue.y == 0) {
        footstepSFX.Stop();
      }
      else {
        if(!footstepSFX.isPlaying) footstepSFX.Play();
      }
    }
  }

  void OnLook(InputValue value) {
    if(canMove) {
      lookValue = value.Get<Vector2>();
      if(isReverse) lookValue *= -1;
    }
  }

  void OnStopCam() { }
}
