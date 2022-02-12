using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] CrosshairType chType;

    [Header("Highlight Color")]
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material originalMaterial;
    [SerializeField] bool useOutline;
    CrosshairController crosshairController;
    Renderer thisRenderer;

    void Start() {
      thisRenderer = GetComponent<Renderer>();
      originalMaterial = thisRenderer.material;
      crosshairController = FindObjectOfType<CrosshairController>();
    }

    public void TurnOnTargettingIndications() {
      thisRenderer.material = highlightMaterial;
      crosshairController.showCrosshair(chType);
    }

    public void TurnOffTargettingIndications() {
      thisRenderer.material = originalMaterial;
      crosshairController.hideAllCrosshairs();
    }
}