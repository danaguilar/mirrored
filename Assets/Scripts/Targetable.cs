using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField]  float outlineWidth = 2f;
    [SerializeField] Color outlineColor = Color.cyan;
    
    Outline outline;

    void Start()
    {
      outline = GetComponent<Outline>();
    }

    public void ActivateTargetedOutline() {
      outline.enabled = true;
    }

    public void DeactivateTargetedOutline() {
      outline.enabled = false;
    }
}