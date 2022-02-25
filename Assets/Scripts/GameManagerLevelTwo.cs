using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelTwo : MonoBehaviour
{

  [SerializeField] int furnitureToMove;
  [SerializeField] EndLevelCinematic endLevelCinematic;
  int furnitureScore = 0;
  // Start is called before the first frame update
  void Start() {
  }

  public void scoreFurniture() {
    furnitureScore++;
    if(furnitureScore >= furnitureToMove) {
      endLevelCinematic.SuccessSequence(PlayerPersister.GetPlayerMovement());
    }
  }
}
