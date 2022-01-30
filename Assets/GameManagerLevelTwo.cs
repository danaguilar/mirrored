using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelTwo : MonoBehaviour
{
  [SerializeField] int furnitureToMove;
  int furnitureScore;
  // Start is called before the first frame update
  void Start() {
    furnitureScore = 0;
      
  }

  public void scoreFurniture() {
    furnitureScore++;
    if(furnitureScore >= furnitureToMove) {
      // Success Sequence and load next level
    }
  }

}
