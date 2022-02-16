using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelTwoEndingCinematic : EndLevelCinematic
{
  [SerializeField] Transform unlockedDoorLocation;
  [SerializeField] float transitionTime;
  [SerializeField] string nameOfLevelToLoad;
  public override void  SuccessSequence(PlayerMovement playerMovement) {
    ForceFocus forceFocus = playerMovement.GetComponent<ForceFocus>();
    playerMovement.DenyMovement();
    // Play Unlock Sound
    forceFocus.LookAt(unlockedDoorLocation, transitionTime);
    LeanTween.move(gameObject, gameObject.transform, transitionTime).setOnComplete(() => LoadNextLevel());
  }

  private void LoadNextLevel() {
    SceneManager.LoadScene(nameOfLevelToLoad);
  }
}
