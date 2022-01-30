using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainVictory : MonoBehaviour, IVictoryCondition
{
  [SerializeField] float scaleAmount;
  [SerializeField] float timeToScale;
  GameManagerLevelTwo gameManager;
  void Start () {
    gameManager = FindObjectOfType<GameManagerLevelTwo>();
  }
  public void SuccessSequence(PlayerMovement playerMovement) {
    SceneManager.LoadScene("Ending");
  }
}