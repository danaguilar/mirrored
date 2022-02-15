using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FurnitureVictory))]
[RequireComponent(typeof(Targetable))]
public class Pushable : MonoBehaviour, IInteractable
{
  FurnitureVictory furnitureVictory;
  public bool currentlyColliding;

  void Start() {
    furnitureVictory = GetComponent<FurnitureVictory>();
  }

  private void OnTriggerEnter(Collider other) {
    if(other.tag != "Floor") currentlyColliding = true;
  }

   private void OnTriggerExit(Collider other) {
    if(other.tag != "Floor") currentlyColliding = false;
  }

  public void Interact(Grabber player) {
    player.StartPushing();
    transform.parent = player.transform;
  }

  public void LetGo(Grabber player) {
    transform.parent = player.room.transform;
    player.StopPushing();
    player.ReleaseObject();
    // if(furnitureVictory.isWithinWinCondition(transform)) furnitureVictory.SuccessSequence(player.GetPlayerMovement());
  }

  public bool isColliding() {
    return currentlyColliding;
  }
}
