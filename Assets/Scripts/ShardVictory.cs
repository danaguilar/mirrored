using System;
using UnityEngine;

public class ShardVictory : MonoBehaviour, IVictoryCondition
{
  [Header("Components")]
  [SerializeField] Transform playerEndingLocation;
  [SerializeField] Transform shardEndingLocation;
  [SerializeField] Transform parentMirror;
  [Header("Audio")]
  [SerializeField] AudioClip placeShardClip;
  [Header("Timings")]
  [SerializeField] float timeToGetInPosition = 1f;
  [SerializeField] float timeToRotateShard = 1f;
  [SerializeField] float timeToPlaceShard = 1f;

  float cameraXAngle = 0f;
  GameManagerLevelOne gameManager;
  float playerYAngle = 180f;
  float cameraZoomIn = 60;
  float cameraZoomOut = 60;
  PlayerMovement playerMovement;
  Camera playerCamera;

  void Start() {
    gameManager = FindObjectOfType<GameManagerLevelOne>();
  }

  public void SuccessSequence(PlayerMovement pMovement) {
    playerMovement = pMovement;
    playerCamera = playerMovement.playerCamera;
    playerMovement.DenyMovement();
    BeginAnimations();
  }

  private void BeginAnimations() {
    playerMovement.DenyMovement();
    LeanTween.move(playerMovement.gameObject, playerEndingLocation.position,  timeToGetInPosition);
    LeanTween.rotateY(playerMovement.gameObject, playerYAngle,  timeToGetInPosition);
    LeanTween.rotateX(playerCamera.gameObject, cameraXAngle,  timeToGetInPosition).setOnComplete(() => RotateShard());
  }

  private void RotateShard() {
    LeanTween.rotate(gameObject, new Vector3(0, -90, 90), timeToRotateShard).setOnComplete(() => PlaceShard()) ;
  }

  private void PlaceShard() {
    GetComponent<AudioSource>().PlayOneShot(placeShardClip);
    LeanTween.scale(gameObject, Vector3.one, timeToPlaceShard);
    LeanTween.move(gameObject, shardEndingLocation, timeToPlaceShard).setOnComplete(() => DisableShard());
  }

  private void DisableShard() {
    playerMovement.AllowMovement();
    gameObject.transform.parent = parentMirror;
    gameObject.GetComponent<Targetable>().enabled = false;
    gameObject.GetComponent<PickUpable>().enabled = false;
    gameManager.turnInShard();
  }

}