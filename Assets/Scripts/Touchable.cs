using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Targetable))]
[RequireComponent(typeof(IVictoryCondition))]
public class Touchable : MonoBehaviour, IInteractable
{
  IVictoryCondition victoryCondition;

  void Start() {
    victoryCondition = GetComponent<CurtainVictory>();
  }
  public void Interact(Grabber player) {
    victoryCondition.SuccessSequence(player.GetPlayerMovement());
  }

  public void LetGo(Grabber player) { }

  public bool isColliding() {
    return false;
  }
}
