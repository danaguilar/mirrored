using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
  static AudioPlayer audioPlayer;
  void Awake() {
    if(audioPlayer == null) {
      audioPlayer = this;
      DontDestroyOnLoad(audioPlayer);
    }
    else{ 
      Destroy(this);
    }
  }
}
