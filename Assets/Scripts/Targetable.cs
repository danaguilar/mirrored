using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    public void ActivateTargetedOutline() {
      outlineRenderer.enabled = true;
    }

    public void DeactivateTargetedOutline() {
      outlineRenderer.enabled = false;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color) {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation ,transform);
        outlineObject.layer = 7;
        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.GetComponent<Targetable>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        Rigidbody rb = outlineObject.GetComponent<Rigidbody>();
        if(rb != null)  {
          rb.isKinematic = true;
          rb.detectCollisions = false;
        }
        rend.enabled = false;

        return rend;
    }

    private void OnMouseEnter()
    {
        outlineRenderer.enabled = true;
    }

    private void OnMouseExit()
    {
        outlineRenderer.enabled = false;
    }
}