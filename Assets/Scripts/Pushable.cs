using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FurnitureVictory))]
[RequireComponent(typeof(Targetable))]
public class Pushable : MonoBehaviour, IInteractable
{
  [SerializeField] AudioClip pushingAudio;
  FurnitureVictory furnitureVictory;
  GameObject room;

  void Start() {
    furnitureVictory = GetComponent<FurnitureVictory>();
    room = FindObjectOfType<RoomIdentifier>().gameObject;
  }

  public void Interact(Grabber player) {
    player.StartPushing(pushingAudio);
    transform.parent = player.transform;
  }

  public void LetGo(Grabber player) {
    transform.parent = room.transform;
    player.StopPushing();
    player.ReleaseObject();
    if(furnitureVictory.isWithinWinCondition(transform)) furnitureVictory.SuccessSequence(player.GetPlayerMovement());
  }

  public bool isColliding() {
    return false;
  }
}
