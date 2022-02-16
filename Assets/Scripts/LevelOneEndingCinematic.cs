using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelOneEndingCinematic : EndLevelCinematic
{
  [SerializeField] RectTransform fadePanel;
  [SerializeField] float timeToFade = 1f;
  [SerializeField] string nameOfLevelToLoad;
  public override void SuccessSequence(PlayerMovement playerMovement) {
    playerMovement.DenyMovement();
    Image panelImage = fadePanel.GetComponent<Image>();
    LeanTween.alpha(fadePanel, 1, timeToFade).setOnComplete(() => LoadNextLevel(playerMovement));
  }

  private void LoadNextLevel(PlayerMovement playerMovement) {
    playerMovement.AllowMovement();
    SceneManager.LoadScene(nameOfLevelToLoad);
  }
}