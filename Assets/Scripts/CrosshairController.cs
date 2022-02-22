using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CrosshairType {
  arrow,
  eye,
  dArrow
}
public class CrosshairController : MonoBehaviour {
  [SerializeField] RawImage arrowCrosshair;
  [SerializeField] RawImage eyeCrosshair;
  [SerializeField] RawImage downArrowCorsshair;

  public void showCrosshair(CrosshairType type) {
    hideAllCrosshairs();
    switch(type) {
      case CrosshairType.arrow:
        arrowCrosshair.enabled = true;
        break;
      case CrosshairType.eye:
        eyeCrosshair.enabled = true;
        break;
      case CrosshairType.dArrow:
        downArrowCorsshair.enabled = true;
        break;
    }
  }

  public void hideAllCrosshairs() {
    arrowCrosshair.enabled = false;
    eyeCrosshair.enabled = false;
    downArrowCorsshair.enabled = false;

  }

  void Start() {
    hideAllCrosshairs();
  }
}
