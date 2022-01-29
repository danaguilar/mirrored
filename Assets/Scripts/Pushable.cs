using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
