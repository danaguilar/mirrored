using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoDoorSlider : MonoBehaviour, IInteractable
{
  [SerializeField] Transform leftDoor;
  [SerializeField] Transform rightDoor;
  [SerializeField] float distanceToMoveDoors;
  [SerializeField] float timeToGetInPosition = 1f;
  [SerializeField] float timeToZoomIn = 1f;
  [SerializeField] float timeToOpenDoors = 1f;
  [SerializeField] float timeToZoomOut = 1f;

  [SerializeField] PlayerMovement playerMovement;
  [SerializeField] Transform playerEndingLocation;

  float cameraXAngle = 6.2f;
  float playerYAngle = 90f;
  float cameraZoomIn = 30;
  float cameraZoomOut = 60;

  Camera playerCamera;

  void Start() {
    playerCamera = playerMovement.playerCamera;
  }

  public void BeginAnimations() {
    playerMovement.DenyMovement();
    LeanTween.move(playerMovement.gameObject, playerEndingLocation.position,  timeToGetInPosition);
    LeanTween.rotateY(playerMovement.gameObject, playerYAngle,  timeToGetInPosition);
    LeanTween.rotateX(playerCamera.gameObject, cameraXAngle,  timeToGetInPosition).setOnComplete(() => ZoomIn());
  }

  private void ZoomIn() {
    cameraZoomOut = playerCamera.fieldOfView;
    LeanTween.value(playerCamera.gameObject, playerCamera.fieldOfView, cameraZoomIn, timeToZoomIn)
          .setOnUpdate((float val) => {playerCamera.fieldOfView = val; })
          .setOnComplete(() => OpenDoors() );
  }

  private void OpenDoors() {
    LeanTween.moveLocalX(leftDoor.gameObject, distanceToMoveDoors, timeToOpenDoors);
    LeanTween.moveLocalX(rightDoor.gameObject, -distanceToMoveDoors, timeToOpenDoors)
          .setOnComplete(() => ZoomOut() );
  }

  private void ZoomOut() {
    LeanTween.value(playerCamera.gameObject, playerCamera.fieldOfView, cameraZoomOut, timeToZoomOut)
          .setOnUpdate((float val) => {playerCamera.fieldOfView = val; })
          .setOnComplete(() => DisablePiano());
  }

  private void DisablePiano() {
    playerMovement.AllowMovement();
    gameObject.GetComponent<BoxCollider>().enabled = false;
  }

  public void Interact(Grabber player) {
    BeginAnimations();
    player.ReleaseObject();
    GetComponent<Targetable>().enabled = false;
  }

  public void LetGo(Grabber player)
  { }
}
