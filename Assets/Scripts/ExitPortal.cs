using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Targetable))]
public class ExitPortal : MonoBehaviour, IInteractable {
  [Header("Tween Timings")]
  [SerializeField] float timeToMoveAboveTub;
  [SerializeField] float timeToSplash;
  [Header("Next Level Info")]
  [SerializeField] string levelFourName;
  PlayerMovement playerMovement;
  public void Interact(Grabber player) {
    playerMovement = player.GetPlayerMovement();
    playerMovement.DenyMovement();
    removeCollider();
    positionCameraAboveTub();
    // load last level
  }

  public void setTargetable() {
    gameObject.layer = 8;
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
    LeanTween.move(playerMovement.gameObject, new Vector3(playerMovement.transform.position.x, 0.2f, playerMovement.transform.position.z), timeToSplash).setEase(LeanTweenType.easeInExpo)
      .setOnComplete(() => loadNextLevel());
  }
  private void loadNextLevel() {
    // Play splash sound
    SceneManager.LoadScene(levelFourName);
  }

  public bool isColliding() {
    return false;
  }

  public void LetGo(Grabber player) { }
}
