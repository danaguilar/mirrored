using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class IntroManager : MonoBehaviour {
    // Start is called before the first frame update
    [Header("Audio")]
    [SerializeField] AudioClip shatteredMirror;
    [SerializeField] AudioSource SfxPlayer;
    [SerializeField] AudioSource musicPlayer;

    [Header("Visual FXs")]
    [SerializeField] RectTransform fadePanel;
    [SerializeField] Button playButton;
    public void onPlayClick() {
      Debug.Log("Play clicked");
      musicPlayer.Stop();
      SfxPlayer.PlayOneShot(shatteredMirror);
      playButton.interactable = false;
      fadePanel.gameObject.SetActive(true);
      LeanTween.alpha(fadePanel, 1, shatteredMirror.length).setOnComplete(() => LoadNextLevel());
      // Load first level
    }

  private void LoadNextLevel() {
    SceneManager.LoadScene("FinalLevel1");
  }

  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
