using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelOne : MonoBehaviour
{
  [SerializeField] int shardsToTurnIn;
  [SerializeField] EndLevelCinematic endLevelCinematic;
  public int shardsCurrentlyTurnedIn = 0;
  PlayerMovement player;
  // Start is called before the first frame update
  void Start() {
    player = FindObjectOfType<PlayerMovement>();
  }

  // Update is called once per frame

  public void turnInShard() {
    shardsCurrentlyTurnedIn++;
    if(shardsCurrentlyTurnedIn >= shardsToTurnIn) {
      endLevelCinematic.SuccessSequence(player);
    }
  }
}
