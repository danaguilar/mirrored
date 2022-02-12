using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CrosshairType {
  arrow,
  eye
}
public class CrosshairController : MonoBehaviour {
  [SerializeField] RawImage arrowCrosshair;
  [SerializeField] RawImage eyeCrosshair;

  public void showCrosshair(CrosshairType type) {
    hideAllCrosshairs();
    switch(type) {
      case CrosshairType.arrow:
        arrowCrosshair.enabled = true;
        break;
      case CrosshairType.eye:
        eyeCrosshair.enabled = true;
        break;
    }
  }

  public void hideAllCrosshairs() {
    arrowCrosshair.enabled = false;
    eyeCrosshair.enabled = false;

  }

  void Start() {
    hideAllCrosshairs();
  }
}
