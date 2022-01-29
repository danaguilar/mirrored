
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
  public float walkingSpeed = 7.5f;
  public float runningSpeed = 11.5f;
  public float jumpSpeed = 8.0f;
  public float gravity = 20.0f;
  public Camera playerCamera;
  public float lookSpeed = 2.0f;
  public float lookXLimit = 45.0f;

  CharacterController characterController;
  Vector3 moveDirection = Vector3.zero;
  float rotationX = 0;

  bool camControlsActive = true;

  private Vector2 moveValue;
  private Vector2 lookValue;

  [HideInInspector]
  public bool canMove = true;

  void Start()
  {
      characterController = GetComponent<CharacterController>();

      // Lock cursor
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
  }

  void Update()
  {
      // We are grounded, so recalculate move direction based on axes
      Vector3 forward = transform.TransformDirection(Vector3.forward);
      Vector3 right = transform.TransformDirection(Vector3.right);
      // Press Left Shift to run
      bool isRunning = false;
      float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveValue.y : 0;
      float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveValue.x : 0;
      float movementDirectionY = moveDirection.y;
      moveDirection = (forward * curSpeedX) + (right * curSpeedY);
      moveDirection.y = movementDirectionY;

      // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
      // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
      // as an acceleration (ms^-2)
      if (!characterController.isGrounded)
      {
          moveDirection.y -= gravity * Time.deltaTime;
      }

      // Move the controller
      characterController.Move(moveDirection * Time.deltaTime);

      // Player and Camera rotation
      if (canMove)
      {
          rotationX += -lookValue.y * lookSpeed;
          rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
          if(camControlsActive) {
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookValue.x * lookSpeed, 0);
          }
      }
  }
  void OnMove(InputValue value) {
    moveValue = value.Get<Vector2>();
  }

  void OnLook(InputValue value) {
    lookValue = value.Get<Vector2>();
  }

  void OnStopCam() {
    camControlsActive = !camControlsActive;
  }
}
