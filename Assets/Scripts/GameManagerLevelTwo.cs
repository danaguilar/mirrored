using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelTwo : MonoBehaviour
{

  [SerializeField] int furnitureToMove;
  [SerializeField] EndLevelCinematic endLevelCinematic;
  PlayerMovement player;
  int furnitureScore = 0;
  // Start is called before the first frame update
  void Start() {
    player = FindObjectOfType<PlayerMovement>();
  }

  public void scoreFurniture() {
    furnitureScore++;
    Debug.Log("Current Score is:" + furnitureScore);
    if(furnitureScore >= furnitureToMove) {
      endLevelCinematic.SuccessSequence(player);
    }
  }
}
