using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelThree : MonoBehaviour
{

  [SerializeField] int clues;
  PlayerMovement player;
  public int clueScore = 0;
  // Start is called before the first frame update
  void Start() {
    player = FindObjectOfType<PlayerMovement>();
  }

  public void scoreClue() {
    clueScore++;
    Debug.Log("Current Score is:" + clueScore);
    if(clueScore >= clues) {
      // Active level 4 portal
    }
  }
}
