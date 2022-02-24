using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeStarter : MonoBehaviour {
  GameManagerLevelThree gameManager;
  void Start() {
    gameManager = FindObjectOfType<GameManagerLevelThree>();

  }
  private void OnTriggerStay(Collider other) {
    if(other.tag == "Player") {
      gameManager.BeginClues();
      Destroy(gameObject);
    }
  }
}
