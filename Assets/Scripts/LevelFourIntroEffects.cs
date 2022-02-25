using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFourIntroEffects : MonoBehaviour {
  [Header("External Game Objects")]
  [SerializeField] RectTransform fadePanel;

  [Header("Audio Clips")]
  [SerializeField] AudioClip initalReversed;
  [SerializeField] AudioClip loopedReversed;

  [Header("Timings")]
  [SerializeField] float timeToFade;
  PlayerMovement playerMovement;
  MusicPlayer musicPlayer;

  void Start() {
    playerMovement = PlayerPersister.GetPlayerMovement();
    musicPlayer = AudioPersister.persister.GetComponent<MusicPlayer>();
    fadePanel.gameObject.SetActive(true);
    playerMovement.DenyMovement();
    FadeInVolume();
    LeanTween.alpha(fadePanel, 0, timeToFade).setOnComplete(() => StartPlayerReverseMovement());
  }

  private void FadeInVolume() {
    AudioSource musicSource = musicPlayer.GetComponent<AudioSource>();
    float cachedVolume = musicSource.volume;
    musicSource.volume = 0;
    musicPlayer.SetNewMusicClips(initalReversed, loopedReversed);
    musicPlayer.StartMusic();
    LeanTween.value(musicSource.gameObject, 0, cachedVolume, timeToFade)
      .setOnUpdate((float volume) => {
        musicSource.volume = volume;
      }
    );
  }

  void StartPlayerReverseMovement() {
    playerMovement.isReverse = true;
    playerMovement.AllowMovement();
  }
}
