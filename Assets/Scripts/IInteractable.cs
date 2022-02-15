using UnityEngine;

public interface IInteractable {
  void Interact(Grabber player);
  void LetGo(Grabber player);
  bool isColliding();
}