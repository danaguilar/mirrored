using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Targetable))]
public class PickUpable : MonoBehaviour, IInteractable
{
  [SerializeField] Collider goal;
  bool isGoalMet = false;
  public void Interact(Grabber player) {
    Transform holdLocation = player.HoldLocation;
    transform.parent = holdLocation;
    transform.rotation = holdLocation.rotation * Quaternion.Euler(-90, 0, 0);
    GetComponent<Rigidbody>().isKinematic = true;
  }

  public void LetGo(Grabber player) {
    if(!isGoalMet) {
      transform.parent = player.room.transform;
      GetComponent<Rigidbody>().isKinematic = false;
      player.ReleaseObject();
    }
    else {
      Debug.Log("You did it!");
      Destroy(gameObject);
    }
  }

  private void OnTriggerEnter(Collider other) {
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
