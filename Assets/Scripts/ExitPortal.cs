using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Targetable))]
public class ExitPortal : MonoBehaviour, IInteractable {
  [Header("Audio")]
  [SerializeField] AudioClip splashAudioClip;
  [Header("Components")]
  [SerializeField] GameObject clueDisplay;
  [SerializeField] GameObject basicWaterDispaly;
  [SerializeField] GameObject fadePanel;
  [Header("Tween Timings")]
  [SerializeField] float timeToMoveAboveTub;
  [SerializeField] float timeToSplash;
  [Header("Next Level Info")]
  [SerializeField] string levelFourName;
  PlayerMovement playerMovement;
  MusicPlayer musicPlayer;

  public void Interact(Grabber player) {
    playerMovement = player.GetPlayerMovement();
    playerMovement.DenyMovement();
    removeCollider();
    positionCameraAboveTub();
  }

  public void setTargetable() {
    gameObject.layer = 8;
    clueDisplay.SetActive(false);
    basicWaterDispaly.SetActive(true);
  }

  private void removeCollider() {
    GetComponent<BoxCollider>().enabled = false;
  }

  private void positionCameraAboveTub() {
    Vector3 positionOverTub = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
    playerMovement.GetComponent<ForceFocus>().LookAtLaterPosition(positionOverTub, gameObject.transform, timeToMoveAboveTub);
    LeanTween.move(playerMovement.gameObject, positionOverTub,timeToMoveAboveTub)
      .setOnComplete(() => moveToSplash());
  }

  private void moveToSplash() {
    LeanTween.move(playerMovement.gameObject, new Vector3(playerMovement.transform.position.x, 0.1f, playerMovement.transform.position.z), timeToSplash).setEase(LeanTweenType.easeInExpo)
      .setOnComplete(() => StartCoroutine(loadNextLevel()));
  }
  private IEnumerator loadNextLevel() {
    fadePanel.SetActive(true);
    AudioSource.PlayClipAtPoint(splashAudioClip, playerMovement.transform.position);
    musicPlayer = FindObjectOfType<MusicPlayer>();
    musicPlayer.StopMusic();
    yield return new WaitForSeconds(splashAudioClip.length);
    UnpersistPlayer();
    SceneManager.LoadScene(levelFourName);
  }

  private void UnpersistPlayer() {
    PlayerPersister.persister = null;
    SceneManager.MoveGameObjectToScene(playerMovement.gameObject, SceneManager.GetActiveScene());
  }


  public bool isColliding() {
    return false;
  }

  public void LetGo(Grabber player) { }
}
