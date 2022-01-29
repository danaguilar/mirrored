using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

  public Transform HoldLocation;
  public GameObject room;
  float armLength;
  IInteractable grabbedObject;
  GameObject playerCameraObject;
  PlayerMovement playerMovement;

  public void StartPushing() {
    playerMovement.SetIsPushing(true);
  }

  public void StopPushing() {
    playerMovement.SetIsPushing(false);
  }

  public void ReleaseObject() {
    grabbedObject = null;
  }

  void Start() {
    playerCameraObject = transform.GetComponentsInChildren<Camera>()[0].gameObject;
    HoldLocation = playerCameraObject.transform.GetChild(0).transform;
    armLength = HoldLocation.position.z;
    playerMovement = GetComponent<PlayerMovement>();
  }

  void OnGrab() {
    if(!hasGrabbedObject()) {
      Collider foundCollider = GetColliderAtArmLength();
      if(colliderIsInteractable(foundCollider)) {
        grabbedObject.Interact(this);
      }
    }
    else {
      grabbedObject.LetGo(this);
    }
  }

  private bool colliderIsInteractable(Collider foundCollider) {
    if(foundCollider == null) return false;
    grabbedObject = foundCollider.GetComponent<IInteractable>();
    return grabbedObject != null;
  }

  private Collider GetColliderAtArmLength() {
    RaycastHit hit;
    LayerMask layerMask = 255; // This can be used to only register certain objects
    Physics.Raycast(playerCameraObject.transform.position, playerCameraObject.transform.TransformDirection(Vector3.forward), out hit, armLength, layerMask);
    return hit.collider;
  }

  private bool hasGrabbedObject() {
    return grabbedObject != null;
  }

  void FixedUpdate() {
    RaycastHit hit;
    LayerMask layerMask = 255;
    if (Physics.Raycast(playerCameraObject.transform.position, playerCameraObject.transform.TransformDirection(Vector3.forward), out hit, armLength, layerMask, QueryTriggerInteraction.Collide))
    {
      Debug.DrawRay(playerCameraObject.transform.position, playerCameraObject.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
    }
    else {
      Debug.DrawRay(playerCameraObject.transform.position, playerCameraObject.transform.TransformDirection(Vector3.forward) * armLength, Color.white);
    }
  }
}
