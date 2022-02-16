using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextClue : MonoBehaviour, IVictoryCondition {

  [Header("Components")]
  [SerializeField] TextMeshProUGUI clueText;
  [SerializeField] Camera clueCam;
  [SerializeField] Transform lookLocation;
  [SerializeField] GameObject associatedNoteWord;
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
  bool clueFound = false;

  void Start() {
    playerMovement = FindObjectOfType<PlayerMovement>();
    gameManager = FindObjectOfType<GameManagerLevelThree>();
    playerTransform = playerMovement.transform;
    mainCameraTranform = playerTransform.GetComponentInChildren<Camera>().transform;
    goalTransform = clueCam.transform;
  }

  public void SuccessSequence(PlayerMovement playerMovement) {
    playerMovement.DenyMovement();
    positionPlayer();
  }


  void positionPlayer() {
    playerMovement.GetComponent<ForceFocus>().LookAt(lookLocation, playerMovementTransitionTime);
    LeanTween.move(gameObject, gameObject.transform.position, playerMovementTransitionTime).setOnComplete(() => showTextEffects());
  }

  void showTextEffects() {
    LeanTween.value(clueText.gameObject, 0, finalTextAlpha, textTransitionTime)
      .setOnUpdate((float alphaTween) => {
        clueText.color = new Color(clueText.color.r, clueText.color.g, clueText.color.b, alphaTween);
      })
      .setOnComplete(() => score());
  }

  void score() {
    playerMovement.AllowMovement();
    associatedNoteWord.SetActive(true);
    // Play writing sound
    gameManager.scoreClue();
  }

  void Update() {
    if(!clueFound && playerIsWithinTurnin()) {
      clueFound = true;
      SuccessSequence(playerMovement);
    }
  }

  private bool playerIsWithinTurnin() {
    return isWithinRange(playerTransform.position.x, goalTransform.position.x - positionGrace, goalTransform.position.x + positionGrace, "X: ")
      && isWithinRange(playerTransform.position.y, goalTransform.position.y - positionGrace, goalTransform.position.y + positionGrace, "Y: ")
      && isWithinRange(playerTransform.eulerAngles.y, goalTransform.eulerAngles.y - rotationGrace, goalTransform.eulerAngles.y + rotationGrace, "RotY: ")
      && isWithinRange(mainCameraTranform.eulerAngles.x, goalTransform.eulerAngles.x - rotationGrace, goalTransform.eulerAngles.x + rotationGrace, "Rot X: ");
  }

  private bool isWithinRange(float testNum, float min, float max, string rangeType) {
    // Debug.Log($"{rangeType}{testNum - Mathf.Clamp(testNum,min, max)}");
    return Mathf.Clamp(testNum, min, max) == testNum;
  }
}
