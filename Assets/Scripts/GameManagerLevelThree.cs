using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelThree : MonoBehaviour
{

  [Header("Components")]
  RenderTexture clueRenderTexture;

  [Header("Clues")]
  public TextClue activeClue;
  [SerializeField] List<TextClue> clueList = new List<TextClue>();
  [SerializeField] MeshRenderer clueCamRenderer;
  PlayerMovement player;
  ExitPortal exitPortal;

  void Start() {
    player = FindObjectOfType<PlayerMovement>();
    exitPortal = FindObjectOfType<ExitPortal>();
    clueRenderTexture = (RenderTexture)clueCamRenderer.material.mainTexture;
    setupNewClue();
  }

  private void setupNewClue() {
    activeClue = clueList[UnityEngine.Random.Range(0, clueList.Count)];
    hideAllInactiveClues();
  }

  private void hideAllInactiveClues() {
    clueList.ForEach((clue) => clue.SetAsCurrentClue(clue == activeClue));
  }

  public void scoreClue() {
    clueList.Remove(activeClue);
    if(clueList.Count == 0) {
      exitPortal.setTargetable();
    }
    else {
      setupNewClue();
    }
  }
}
