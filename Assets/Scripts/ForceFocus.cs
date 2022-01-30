using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFocus : MonoBehaviour
{
  [SerializeField] float timeToFocus = 1f;
  [SerializeField] float timeToZoom = 0.5f;
  [SerializeField] float zoomAmmount = 10f;
  public Transform focusPoint;
  PlayerMovement playerMovement;
  GameObject playerCameraObject;

  // Start is called before the first frame update
  void Awake() {
    playerMovement = GetComponent<PlayerMovement>();
    playerCameraObject = transform.GetComponentsInChildren<Camera>()[0].gameObject;
    
  }

  public void StartFocus() {
    playerMovement.DenyMovement();
    Vector3 newRotation =  Quaternion.LookRotation(focusPoint.position - gameObject.transform.position).eulerAngles;
    LeanTween.rotateY(gameObject, newRotation.y,  timeToFocus);
    LeanTween.rotateX(playerCameraObject, newRotation.x,  timeToFocus).setOnComplete(() => {ZoomIn();});
  }

  private void ZoomIn() {
    Camera camera = playerCameraObject.GetComponent<Camera>();
    LeanTween.value(playerCameraObject, camera.fieldOfView, camera.fieldOfView - zoomAmmount, timeToZoom)
          .setOnUpdate((float val) => {camera.fieldOfView = val; })
          .setOnComplete(() => ZoomOut() );
  }

  private void ZoomOut() {
    Camera camera = playerCameraObject.GetComponent<Camera>();
    LeanTween.value(playerCameraObject, camera.fieldOfView, camera.fieldOfView + zoomAmmount, timeToZoom)
          .setOnUpdate((float val) => {camera.fieldOfView = val; })
          .setOnComplete(() => playerMovement.AllowMovement());
  }

  void OnStopCam() {
    StartFocus();
  }
}