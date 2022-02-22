using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
  [SerializeField] AudioClip initialMusic;
  [SerializeField] AudioClip loopedMusic;
  AudioSource audioSource;
  bool keepPlaying = true;

  public void stopMusic() {
    audioSource.Stop();
    keepPlaying = false;
  }

  void Start() {
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = initialMusic;
    audioSource.Play();
  }

  void Update() {
    if(keepPlaying && !audioSource.isPlaying) {
      audioSource.clip = loopedMusic;
      audioSource.Play();
    }
  }
}
