using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Targetable))]
public class PickUpable : MonoBehaviour, IInteractable
{
  [SerializeField] Collider goal;
  ShardVictory shardVictory;
  bool isGoalMet = false;

  Vector3 initialPosition;
  Vector3 initialScale;
  Quaternion initialRotation;

  void Start() {
    initialPosition = transform.position;
    initialScale = transform.localScale;
    initialRotation = transform.rotation;
    
    shardVictory = GetComponent<ShardVictory>();
  }
  public void Interact(Grabber player) {
    Transform holdLocation = player.HoldLocation;
    transform.parent = holdLocation;
    transform.rotation = holdLocation.rotation * Quaternion.Euler(-90, 0, 0);
  }

  public void LetGo(Grabber player) {
    if(!isGoalMet) {
      transform.parent = player.room.transform;
      ResetLocation();
      player.ReleaseObject();
    }
    else {
      PlayerMovement pMovement = player.GetComponent<PlayerMovement>();
      shardVictory.SuccessSequence(pMovement);
      player.ReleaseObject();
    }
  }

  private void ResetLocation() {
    transform.position = initialPosition;
    transform.localScale = initialScale;
    transform.rotation = initialRotation;
  }

  private void OnTriggerEnter(Collider other) {
    Debug.Log("Entering Trigger");
    if(other == goal) {
      Debug.Log("Goal is being met");
      isGoalMet = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if(other == goal) {
      Debug.Log("No Wait! You were so close!");
      isGoalMet = false;
    }
  }
}
