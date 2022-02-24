using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsIntro : MonoBehaviour {
  [SerializeField] RectTransform fadePanel;
  [SerializeField] float timeToFade;
  void Start() {
    fadePanel.gameObject.SetActive(true);
    LeanTween.alpha(fadePanel, 0, timeToFade);
  }
}
