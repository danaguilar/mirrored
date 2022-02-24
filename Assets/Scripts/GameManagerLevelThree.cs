using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevelThree : MonoBehaviour
{

  [Header("Components")]
  RenderTexture clueRenderTexture;
  
  [Header("Audio")]
  [SerializeField] AudioClip scribbleClip;

  [SerializeField] List<AudioClip> audioScribbleClips = new List<AudioClip>();

  [Header("Clues")]
  public TextClue activeClue;
  public AudioClip activeClip;
  [SerializeField] List<TextClue> clueList = new List<TextClue>();
  [SerializeField] MeshRenderer clueCamRenderer;
  ExitPortal exitPortal;

  void Start() {
    exitPortal = FindObjectOfType<ExitPortal>();
    clueRenderTexture = (RenderTexture)clueCamRenderer.material.mainTexture;
  }

  public void BeginClues() {
    setupNewClue();
  }

  private void setupNewClue() {
    int randomSelection = UnityEngine.Random.Range(0, clueList.Count);
    activeClue = clueList[randomSelection];
    activeClip = audioScribbleClips[randomSelection];
    hideAllInactiveClues();
  }

  private void hideAllInactiveClues() {
    clueList.ForEach((clue) => clue.SetAsCurrentClue(clue == activeClue));
  }

  public void scoreClue() {
    PlayerMovement player = FindObjectOfType<PlayerMovement>();
    AudioSource.PlayClipAtPoint(activeClip, player.transform.position);
    clueList.Remove(activeClue);
    audioScribbleClips.Remove(activeClip);
    if(clueList.Count == 0) {
      exitPortal.setTargetable();
    }
    else {
      setupNewClue();
    }
  }
}
