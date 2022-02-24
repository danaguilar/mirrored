using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSfxManager : MonoBehaviour {
  [SerializeField] AudioSource WalkingAudioSource;
  AudioSource CustomAudioSource;

  static WalkSfxManager walkSFX;

  void Awake() {
    if(walkSFX == null) {
      walkSFX = this;
      DontDestroyOnLoad(this);
    }
    else {
      Destroy(gameObject);
    }
  }

  public void SetPushing(AudioSource pushingAS) {
    CustomAudioSource = pushingAS;
  }

  public void StopPushing() {
    CustomAudioSource = null;
  }

  public void PlayWalkSFX() {
    PlayWithNoInterruptions(CustomAudioSource != null ? CustomAudioSource : WalkingAudioSource);
  }

  private void PlayWithNoInterruptions(AudioSource AS) {
    if(!AS.isPlaying) AS.Play();
  }

  public void StopWalkSFX() {
    WalkingAudioSource.Stop();
    if(CustomAudioSource != null) CustomAudioSource.Stop();
  }
}
