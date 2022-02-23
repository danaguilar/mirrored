using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelTwoEndingCinematic : EndLevelCinematic
{
  [SerializeField] Transform unlockedDoorLocation;
  [SerializeField] AudioClip openningDoorAudioClip;
  [SerializeField] string nameOfLevelToLoad;
  public override void  SuccessSequence(PlayerMovement playerMovement) {
    ForceFocus forceFocus = playerMovement.GetComponent<ForceFocus>();
    playerMovement.DenyMovement();
    AudioSource.PlayClipAtPoint(openningDoorAudioClip, unlockedDoorLocation.position);
    forceFocus.LookAt(unlockedDoorLocation, openningDoorAudioClip.length, LeanTweenType.easeOutQuint);
    LeanTween.move(gameObject, gameObject.transform, openningDoorAudioClip.length).setOnComplete(() => LoadNextLevel());
  }

  private void LoadNextLevel() {
    SceneManager.LoadScene(nameOfLevelToLoad);
  }
}
