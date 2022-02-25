using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Targetable))]
public class ExitDoor : MonoBehaviour, IInteractable {
  [Header("Components")]
  [SerializeField] RectTransform fadePanel;
  [SerializeField] string endingScreenName;
  [Header("Audio")]
  [SerializeField] AudioClip openDoorClip;

  void IInteractable.Interact(Grabber player) {
    fadeOut();
  }

  private void fadeOut() {
    FindObjectOfType<MusicPlayer>().StopMusic();
    PlayerPersister.GetPlayerMovement().DenyMovement();
    AudioSource.PlayClipAtPoint(openDoorClip, transform.position);
    LeanTween.alpha(fadePanel, 1, openDoorClip.length).setOnComplete(() => LoadNextLevel());
  }

  private void LoadNextLevel() {
    SceneManager.LoadScene(endingScreenName);
  }

  bool IInteractable.isColliding() { 
    return false;
  }

  void IInteractable.LetGo(Grabber player)
  { }
}
