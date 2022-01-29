using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Targetable))]
public class Pushable : MonoBehaviour, IInteractable
{
  public void Interact(Grabber player) {
    player.StartPushing();
    transform.parent = player.transform;
  }

  public void LetGo(Grabber player) {
    transform.parent = player.room.transform;
    player.StopPushing();
    player.ReleaseObject();
  }
}
