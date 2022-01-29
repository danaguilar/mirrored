using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpable : MonoBehaviour, IInteractable
{
  public void Interact(Grabber player) {
    Transform holdLocation = player.HoldLocation;
    transform.parent = holdLocation;
    transform.rotation = holdLocation.rotation * Quaternion.Euler(-90, 0, 0);
    GetComponent<Rigidbody>().isKinematic = true;
  }

  public void LetGo(Grabber player) {
    transform.parent = player.room.transform;
    GetComponent<Rigidbody>().isKinematic = false;
    player.ReleaseObject();
  }
}
