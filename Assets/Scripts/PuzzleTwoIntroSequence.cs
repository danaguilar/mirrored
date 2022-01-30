using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTwoIntroSequence : MonoBehaviour, IVictoryCondition
{
  [SerializeField] RectTransform fadePanel;
  [SerializeField] float timeToFade = 1f;

  PlayerMovement playerMovement;
  // Start is called before the first frame update
  void Start() {
    playerMovement = FindObjectOfType<PlayerMovement>();
    SuccessSequence(playerMovement);
    
  }
  public void SuccessSequence(PlayerMovement playerMovement) {
    playerMovement.DenyMovement();
    Image panelImage = fadePanel.GetComponent<Image>();
    LeanTween.alpha(fadePanel, 0, timeToFade).setOnComplete(() => SetupPuzzleTwo());
  }

  private void SetupPuzzleTwo() {
    playerMovement.AllowMovement();
  }
}
