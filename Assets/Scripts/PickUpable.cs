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

  AudioSource pickupSFX;

  Vector3 initialPosition;
  Vector3 initialScale;
  Quaternion initialRotation;

  void Start() {
    initialPosition = transform.position;
    initialScale = transform.localScale;
    initialRotation = transform.rotation;
    
    shardVictory = GetComponent<ShardVictory>();
    pickupSFX = GetComponent<AudioSource>();
  }
  public void Interact(Grabber player) {
    Transform holdLocation = player.HoldLocation;
    pickupSFX.Play();
    transform.parent = holdLocation;
    transform.rotation = holdLocation.rotation * Quaternion.Euler(-90, 0, 0);
    transform.position = holdLocation.position;
  }

  public void LetGo(Grabber player) {
    if(!isGoalMet) {
      Debug.Log($"Grabber game object: {player}", player);
      transform.parent = player.room.transform;
      ResetLocation();
      player.ReleaseObject();
    }
    else {
      PlayerMovement pMovement = player.GetComponent<PlayerMovement>();
      gameObject.layer = 0;
      shardVictory.SuccessSequence(pMovement);
      player.ReleaseObject();
    }
  }
  
  public bool isColliding() {
    return false;
  }

  private void ResetLocation() {
    transform.position = initialPosition;
    transform.localScale = initialScale;
    transform.rotation = initialRotation;
  }

  private void OnTriggerEnter(Collider other) {
    if(other == goal) {
      isGoalMet = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if(other == goal) {
      isGoalMet = false;
    }
  }
}
