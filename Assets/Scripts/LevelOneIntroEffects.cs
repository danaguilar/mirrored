using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneIntroEffects : MonoBehaviour {
  [SerializeField] RectTransform fadePanel;
  [SerializeField] float timeToFade;
  [SerializeField] bool setReversed;
  PlayerMovement playerMovement;
  MusicPlayer musicPlayer;

  void Start() {
    playerMovement = PlayerPersister.GetPlayerMovement();
    musicPlayer = FindObjectOfType<MusicPlayer>();
    fadePanel.gameObject.SetActive(true);
    playerMovement.DenyMovement();
    FadeInVolume();
    LeanTween.alpha(fadePanel, 0, timeToFade).setOnComplete(() => InitialMusicAndPlayer());
  }

  private void FadeInVolume() {
    AudioSource musicSource = musicPlayer.GetComponent<AudioSource>();
    float cachedVolume = musicSource.volume;
    musicSource.volume = 0;
    musicPlayer.StartMusic();
    LeanTween.value(musicSource.gameObject, 0, cachedVolume, timeToFade)
      .setOnUpdate((float volume) => {
        musicSource.volume = volume;
      }
    );
  }

  void InitialMusicAndPlayer() {
    playerMovement.isReverse = setReversed;
    playerMovement.AllowMovement();
  }
}
