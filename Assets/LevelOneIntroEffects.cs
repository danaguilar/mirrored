using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneIntroEffects : MonoBehaviour {
  [SerializeField] RectTransform fadePanel;
  [SerializeField] float timeToFade;
  PlayerMovement playerMovement;
  MusicPlayer musicPlayer;

  void Awake() {
    playerMovement = FindObjectOfType<PlayerMovement>();
    musicPlayer = FindObjectOfType<MusicPlayer>();
  }

  void Start() {
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
    playerMovement.AllowMovement();
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
