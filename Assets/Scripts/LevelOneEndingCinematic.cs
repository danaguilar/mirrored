using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelOneEndingCinematic : EndLevelCinematic
{
  [SerializeField] RectTransform fadePanel;
  [SerializeField] string nameOfLevelToLoad;

  AudioSource mirrorRestoredSFX;

  void Start() {
    mirrorRestoredSFX = GetComponent<AudioSource>();
  }
  public override void SuccessSequence(PlayerMovement playerMovement) {
    playerMovement.DenyMovement();
    mirrorRestoredSFX.Play();
    LeanTween.alpha(fadePanel, 1, mirrorRestoredSFX.clip.length).setOnComplete(() => LoadNextLevel(playerMovement));
  }

  private void LoadNextLevel(PlayerMovement playerMovement) {
    playerMovement.AllowMovement();
    SceneManager.LoadScene(nameOfLevelToLoad);
  }
}
