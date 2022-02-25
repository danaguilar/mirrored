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
    if(audioSource == null) audioSource = GetComponent<AudioSource>();
    audioSource.clip = initialMusic;
    audioSource.Play();
    keepPlaying = true;
  }

  public void SetNewMusicClips(AudioClip initial, AudioClip looped) {
    initialMusic = initial;
    loopedMusic = looped;
  }

  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void Update() {
    if(keepPlaying && !audioSource.isPlaying) {
      audioSource.clip = loopedMusic;
      audioSource.Play();
    }
  }
}
