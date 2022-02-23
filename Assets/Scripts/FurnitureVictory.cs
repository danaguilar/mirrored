using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FurnitureVictory : MonoBehaviour, IVictoryCondition
{
  [SerializeField] Transform mirroredFurniture;
  [SerializeField] float positionGrace = 100f;
  [SerializeField] float rotationGrace = 180f;
  [SerializeField] float transitionTime = 1f;
  GameManagerLevelTwo gameManager;
  AudioSource victoryAudio;


  void Start() {
    gameManager = FindObjectOfType<GameManagerLevelTwo>();
    victoryAudio = GetComponent<AudioSource>();
  }

  public void SuccessSequence(PlayerMovement playerMovement) {
    ForceFocus forceFocus = playerMovement.GetComponent<ForceFocus>();
    victoryAudio.Play();
    playerMovement.DenyMovement();
    LeanTween.rotate(gameObject, mirroredFurniture.rotation.eulerAngles, victoryAudio.clip.length);
    forceFocus.LookAt(mirroredFurniture.transform, victoryAudio.clip.length);
    LeanTween.move(gameObject, mirroredFurniture.position, victoryAudio.clip.length).setOnComplete(() => DisableFurnature(playerMovement));
  }

  private void DisableFurnature(PlayerMovement playerMovement) {
    gameManager.scoreFurniture();
    GetComponent<BoxCollider>().enabled = false;
    playerMovement.AllowMovement();
  }

  public bool isWithinWinCondition(Transform furniture) {
    Debug.Log("Checking Victory");
    return isWithinPosition(furniture);
  }

  private bool isWithinRotation(Transform transform) {
    return isWithinRange(transform.rotation.eulerAngles.z, mirroredFurniture.transform.rotation.eulerAngles.z - rotationGrace, mirroredFurniture.transform.rotation.eulerAngles.z + rotationGrace)
      && isWithinRange(transform.rotation.eulerAngles.y, mirroredFurniture.transform.rotation.eulerAngles.y - rotationGrace, mirroredFurniture.transform.rotation.eulerAngles.y + rotationGrace);
  }


  private bool isWithinPosition(Transform transform) {
    bool victory = isWithinRange(transform.position.x, mirroredFurniture.transform.position.x - positionGrace, mirroredFurniture.transform.position.x + positionGrace)
      && isWithinRange(transform.position.z, mirroredFurniture.transform.position.z - positionGrace, mirroredFurniture.transform.position.z + positionGrace);
      if(!victory) {
        Debug.Log("Furniture position: " + transform.position);
        Debug.Log("Target position: " + mirroredFurniture.transform.position);
        Debug.Log("Furniture rotation: " + transform.rotation.eulerAngles);
        Debug.Log("Target rotation: " + mirroredFurniture.rotation.eulerAngles);
        return false;
      }
      return true;
  }

  private bool isWithinRange(float testNum, float min, float max) {
    return Mathf.Clamp(testNum, min, max) == testNum;
  }
}
