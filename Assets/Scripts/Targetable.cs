using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] float outlineWidth = 2f;
    [SerializeField] Color outlineColor = Color.cyan;
    [SerializeField] CrosshairType chType;
    CrosshairController crosshairController;
    
    Outline outline;

    void Start() {
      outline = GetComponent<Outline>();
      crosshairController = FindObjectOfType<CrosshairController>();
      outline.enabled = false;
      outline.OutlineColor = outlineColor;
      outline.OutlineWidth = outlineWidth;
    }

    public void TurnOnTargettingIndications() {
      Debug.Log("ON");
      outline.enabled = true;
      crosshairController.showCrosshair(chType);
    }

    public void TurnOffTargettingIndications() {
      Debug.Log("OFF");
      outline.enabled = false;
      crosshairController.hideAllCrosshairs();
    }
}