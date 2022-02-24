using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
  [SerializeField] AudioClip initialMusic;
  [SerializeField] AudioClip loopedMusic;
  AudioSource audioSource;
  public bool keepPlaying = false;

  public void StopMusic() {
    audioSource.Stop();
    keepPlaying = false;
  }

  public void StartMusic() {
    audioSource.Play();
    keepPlaying = true;
  }

  void Start() {
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = initialMusic;
  }

  void Update() {
    if(keepPlaying && !audioSource.isPlaying) {
      audioSource.clip = loopedMusic;
      audioSource.Play();
    }
  }
}
