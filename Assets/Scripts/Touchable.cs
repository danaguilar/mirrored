using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Targetable))]
public class Touchable : MonoBehaviour, IInteractable
{
  CurtainVictory curtainVictory;

  void Start() {
    curtainVictory = GetComponent<CurtainVictory>();
  }
  public void Interact(Grabber player) {
    curtainVictory.SuccessSequence(player.GetPlayerMovement());
  }

  public void LetGo(Grabber player) { }
  public bool isColliding() {
    return false;
  }
}
