using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelTwo : MonoBehaviour
{

  [SerializeField] int furnitureToMove;
  [SerializeField] GameObject realExit;
  int furnitureScore;
  // Start is called before the first frame update
  void Start() {
    furnitureScore = 0;
      
  }

  public void scoreFurniture() {
    furnitureScore++;
    Debug.Log("Current Score is:" + furnitureScore);
    if(furnitureScore >= furnitureToMove) {
      realExit.SetActive(true);
    }
  }
}
