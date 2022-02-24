using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextClue : MonoBehaviour, IVictoryCondition {

  [Header("Components")]
  [SerializeField] TextMeshProUGUI clueText;
  public Camera clueCamera;
  [SerializeField] Transform lookLocation;
  [SerializeField] GameObject associatedNoteWord;
  [SerializeField] Transform sinkWater;
  [SerializeField] bool isSink;

  [Header("Audio")]
  [SerializeField] AudioClip successClip;

  [Header("Transition Values")]
  [SerializeField] float playerMovementTransitionTime;
  [SerializeField] float textTransitionTime;
  [SerializeField] float finalTextAlpha;

  [Header("Grace Amounts")]
  [SerializeField] float positionGrace;
  [SerializeField] float rotationGrace;
  PlayerMovement playerMovement;
  Transform playerTransform;
  Transform mainCameraTranform;
  Transform goalTransform;
  GameManagerLevelThree gameManager;
  bool isActive = false;
  bool alreadyFired = false;

  void Start() {
    playerMovement = FindObjectOfType<PlayerMovement>();
    gameManager = FindObjectOfType<GameManagerLevelThree>();
    playerTransform = playerMovement.transform;
    mainCameraTranform = clueCamera.transform;
    goalTransform = clueCamera.transform;
  }

  public void SuccessSequence(PlayerMovement playerMovement) {
    playerMovement.DenyMovement();
    positionPlayer();
  }
  
  public void SetAsCurrentClue(bool isCurrentClue) {
    clueCamera.enabled = isCurrentClue;
    isActive = isCurrentClue;
  }

  void positionPlayer() {
    playerMovement.GetComponent<ForceFocus>().LookAt(lookLocation, playerMovementTransitionTime, LeanTweenType.easeOutQuint);
    LeanTween.move(gameObject, gameObject.transform.position, playerMovementTransitionTime).setOnComplete(() => showTextEffects());
  }

  void showTextEffects() {
    AudioSource.PlayClipAtPoint(successClip, playerMovement.transform.position);
    if(isSink) {
      LeanTween.move(sinkWater.gameObject, new Vector3(sinkWater.position.x,sinkWater.position.y -0.3f ,sinkWater.position.z), textTransitionTime).setOnComplete(() => score());
    }
    else {
      LeanTween.value(clueText.gameObject, 0, finalTextAlpha, textTransitionTime)
        .setOnUpdate((float alphaTween) => {
          clueText.color = new Color(clueText.color.r, clueText.color.g, clueText.color.b, alphaTween);
        })
        .setEase(LeanTweenType.easeOutElastic)
        .setOnComplete(() => score());
    }
  }

  void score() {
    playerMovement.AllowMovement();
    associatedNoteWord.SetActive(true);
    // Play writing sound
    gameManager.scoreClue();
  }

  void Update() {
    if(isActive && !alreadyFired && playerIsWithinTurnin()){
      alreadyFired = true;
      SuccessSequence(playerMovement);
    } 
  }

  private bool playerIsWithinTurnin() {
    if(playerTransform == null) GetPlayerMovement();
    return isWithinRange(playerTransform.position.x, goalTransform.position.x - positionGrace, goalTransform.position.x + positionGrace, "X: ")
      && isWithinRange(playerTransform.position.z, goalTransform.position.z - positionGrace, goalTransform.position.z + positionGrace, "Z: ")
      && isWithinRange(playerTransform.eulerAngles.y, goalTransform.eulerAngles.y - rotationGrace, goalTransform.eulerAngles.y + rotationGrace, "RotY: ")
      && isWithinRange(mainCameraTranform.eulerAngles.x, goalTransform.eulerAngles.x - rotationGrace, goalTransform.eulerAngles.x + rotationGrace, "Rot X: ");
  }

  private void GetPlayerMovement() {
    if(playerMovement == null) playerMovement = FindObjectOfType<PlayerMovement>();
    playerTransform = playerMovement.transform;
  }

  private bool isWithinRange(float testNum, float min, float max, string rangeType) {
    return Mathf.Clamp(testNum, min, max) == testNum;
  }
}
