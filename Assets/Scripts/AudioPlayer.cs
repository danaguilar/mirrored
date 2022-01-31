using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
  static AudioPlayer audioPlayer;
  // Start is called before the first frame update
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
